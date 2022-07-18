using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables that need assigning
    public CharacterController playerCharacterController;
    public FeetCollider playerFeet;

    // Variables that need adjusting
    public float jumpPower = 2;
    public float fallSpeed = 5;
    public float jumpHoldTime = 1;

    // Private Variables
    [SerializeField] private float ySpeed, stepOffset, timeFromJump;
    [SerializeField] private bool isGrounded, pressingJump, falling;

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

    public void PressingJump(bool x)
    {
        // Public function so that the bool can be changed in unity button UI
        pressingJump = x;
    }
}
