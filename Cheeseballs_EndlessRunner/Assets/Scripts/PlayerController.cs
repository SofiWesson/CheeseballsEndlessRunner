using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables that need assigning
    public CharacterController playerCharacterController;
    public FeetCollider playerFeet;
    public Animator characterAnimator;
    public GameManager gameManager;

    // Variables that need adjusting
    public float jumpPower = 5;
    public float fallSpeed = 20;
    public float jumpHoldTime = 0.3f;
    public float minimumSlideTime = 0.2f;
    public float maximumSlideTime = 1;
    public float slideDelayTime = 0.5f;
    public float jumpDelayTime = 0.5f;

    // Private Variables
    [SerializeField] private float ySpeed, stepOffset, jumpTime, slideTime, slideDelay, jumpDelay, playerHeight;
    [SerializeField] private bool isGrounded, isFalling, pressingJump, pressingSlide, isSliding;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the step off set to what it is set in Unity
        stepOffset = playerCharacterController.stepOffset;
        playerHeight = playerCharacterController.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VerticalMovement();
        Movement();
        if (isSliding)
        {
            Sliding();
        }
        // Begins sliding if player presses slide and delay for standing is finished
        else if (pressingSlide && slideDelay == 0 && ySpeed == 0 && jumpDelay == 0)
        {
            Slide();
        }
        // decreases slide and jump delay if necessary. Idk what the efficient way to do this is, I'm a designer
        if (slideDelay > 0)
        {
            slideDelay -= Time.fixedDeltaTime;
            if (slideDelay < 0)
            {
                slideDelay = 0;
                StandHitbox();
            }
        }
        if (jumpDelay > 0)
        {
            jumpDelay -= Time.fixedDeltaTime;
            if (jumpDelay < 0)
            {
                jumpDelay = 0;
            }
        }

        characterAnimator.SetBool("isGrounded", isGrounded);
        characterAnimator.SetBool("isSliding", isSliding);
    }
    void Movement()
    {
        // Connects to the character controller and makes the player move
        playerCharacterController.Move(transform.up * ySpeed * Time.fixedDeltaTime);
    }

    void VerticalMovement()
    {
        isGrounded = playerFeet.isGrounded;

        // Checks if the player can jump
        if (pressingJump && !isFalling && !isSliding && jumpDelay == 0 && slideDelay == 0)
        {
            if (ySpeed == 0)
            {
                // Begins jumping animation for the character
                characterAnimator.SetTrigger("jump");
            }

            // Starts the jump and prevents stepping
            ySpeed = jumpPower;
            playerCharacterController.stepOffset = 0;
        }

        if (isGrounded == false)
        {
            jumpTime += Time.fixedDeltaTime;
            // Checks if player is not holding jump or has held jump for too long
            if (!pressingJump || jumpTime >= jumpHoldTime)
            {
                isFalling = true;
                // Makes the player fall and prevents stepping
                ySpeed -= fallSpeed * Time.fixedDeltaTime;
                playerCharacterController.stepOffset = 0;
            }
        }

        // Checks if player lands from a jump
        if (isGrounded == true && ySpeed < 0)
        {
            // Removes falling speed and enables stepping
            ySpeed = 0;
            playerCharacterController.stepOffset = stepOffset;
            jumpTime = 0;
            isFalling = false;
            jumpDelay = jumpDelayTime;
        }
    }

    private void Sliding()
    {
        slideTime += Time.fixedDeltaTime;
        // will not make the player stand up until they slide for a minimum amount of time
        if (slideTime < minimumSlideTime)
        {
            return;
        }
        // If player lets go of slide button, or they have been sliding for too long, they stand up
        else if (!pressingSlide || slideTime >= maximumSlideTime)
        {
            Stand();
        }
    }

    private void Slide()
    {
        characterAnimator.SetTrigger("slide");

        // Turns on crouch mesh and adjusts hitbox
        playerCharacterController.height = playerHeight / 2;
        playerCharacterController.center = new Vector3(0, -0.5f, 0);

        isSliding = true;
        slideTime += Time.fixedDeltaTime;
    }

    private void Stand()
    {
        isSliding = false;
        slideTime = 0;
        slideDelay = slideDelayTime;
    }

    private void StandHitbox()
    {
        // Adjusts Hitbox
        playerCharacterController.height = playerHeight;
        playerCharacterController.center = Vector3.zero;
    }

    public void PressingJump()
    {
        // Public function so that the bool can be changed in unity button UI
        pressingJump = true;
    }

    public void ReleasingJump()
    {
        pressingJump = false;
    }    

    public void PressingSlide()
    {
        // Public function so that the bool can be changed in unity button UI
        pressingSlide = true;
    }

    public void ReleasingSlide()
    {
        pressingSlide = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger")
        {
            gameManager.EndGame();
        }
    }
}