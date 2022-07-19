using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables that need assigning
    public CharacterController playerCharacterController;
    public FeetCollider playerFeet;
    public GameObject runningMesh, slidingMesh;

    // Variables that need adjusting
    public float jumpPower = 2;
    public float fallSpeed = 5;
    public float jumpHoldTime = 1;
    public float minimumSlideTime = 0.2f;
    public float maximumSlideTime = 1;
    public float slideDelayTime = 0.5f;

    // Private Variables
    [SerializeField] private float ySpeed, stepOffset, timeFromJump, slideTime, slideDelay;
    [SerializeField] private bool isGrounded, falling, pressingJump, pressingSlide, pressedSlide, sliding;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the step off set to what it is set in Unity
        stepOffset = playerCharacterController.stepOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VerticalMovement();
        Movement();
        if (sliding)
        {
            Sliding();
        }
        // Begins sliding if player presses slide and delay for standing is finished
        else if (pressedSlide && slideDelay == 0)
        {
            Slide();
        }
        if (pressedSlide)
        {
            pressedSlide = false;
        }
        // decreases slide delay if necessary. Idk what the efficient way to do this is, I'm a designer
        if (slideDelay > 0)
        {
            slideDelay -= Time.fixedDeltaTime;
            if (slideDelay < 0)
            {
                slideDelay = 0;
            }
        }
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
        if (pressingJump && !falling)
        {
            // Starts the jump and prevents stepping
            ySpeed = jumpPower;
            playerCharacterController.stepOffset = 0;
        }

        if (isGrounded == false)
        {
            timeFromJump += Time.fixedDeltaTime;
            // Checks if player is not holding jump or has held jump for too long
            if (!pressingJump || timeFromJump >= jumpHoldTime)
            {
                falling = true;
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
            timeFromJump = 0;
            falling = false;
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
        // Turns on crouch mesh and adjusts hitbox
        runningMesh.SetActive(false);
        slidingMesh.SetActive(true);
        playerCharacterController.height = 1;
        playerCharacterController.center = new Vector3(0, -0.5f, 0);

        sliding = true;
        slideTime += Time.fixedDeltaTime;
    }

    private void Stand()
    {
        // Turns on running mesh and adjusts hitbox
        runningMesh.SetActive(true);
        slidingMesh.SetActive(false);
        playerCharacterController.height = 2;
        playerCharacterController.center = Vector3.zero;

        sliding = false;
        slideTime = 0;
        slideDelay = slideDelayTime;
    }

    public void PressingJump()
    {
        // Public function so that the bool can be changed in unity button UI
        pressingJump = !pressingJump;
    }

    public void PressingSlide()
    {
        // Public function so that the bool can be changed in unity button UI
        pressingSlide = !pressingSlide;
        if (pressingSlide)
        {
            pressedSlide = true;
        }
    }
}
