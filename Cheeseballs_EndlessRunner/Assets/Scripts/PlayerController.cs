using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public CharacterController characterController;
    [SerializeField] private bool jumpActive;
    [SerializeField] private bool slideActive;
    [SerializeField] private float ySpeed;
    [SerializeField] private float jumpTime;

    // public Button jumpButton;
    // public Button slideButton;

    // Start is called before the first frame update
    void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (jumpActive)
        {
            Jump();
        }
        else
        {
            Falling();
        }

        characterController.Move(Vector3.up * ySpeed * Time.fixedDeltaTime);
    }
    public void ActivateJump()
    {
        jumpActive = true;
    }
    public void DeactivateJump()
    {
        jumpActive = false;
    }

    private void Jump()
    {
        jumpTime += Time.fixedDeltaTime;
        if (jumpTime >= 1)
        {
            return;
        }
        ySpeed += jumpHeight;
    }

    private void Falling()
    {
        ySpeed -= jumpHeight * gravity;
    }

    private void Landing()
    {
        jumpTime = 0;
    }

    public void Slide()
    {

    }
}
