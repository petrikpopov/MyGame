using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class CharacterScript : MonoBehaviour
{
    private Animator animator;
     private InputAction moveAction;
    private InputAction jumpAction;
     private InputAction sprintAction;
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.5f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveState moveState = MoveState.Idle;
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        float sprintValue = sprintAction.ReadValue<float>();
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        if(cameraForward != Vector3.zero)
        {
            cameraForward.Normalize();
        }
        Vector3 moveStep = playerSpeed * Time.deltaTime * (1+2+sprintValue) * (
            moveValue.x * Camera.main.transform.right +
            moveValue.y * cameraForward
        );
        if(moveStep.magnitude > 0f) {
            moveState = sprintValue != 0 ? MoveState.Run : Mathf.Abs(moveValue.x ) >  Mathf.Abs(moveValue.y) ? MoveState.SideWalk : MoveState.Walk; 
        }
        this.transform.forward = cameraForward;
        characterController.Move(moveStep);
        // Makes the player jump
        if (jumpAction.ReadValue<float>() > 0 && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        animator.SetInteger("MoveState", (int)moveState); 
    }
}

enum MoveState {
    Idle = 1,
    Walk = 2,
    SideWalk = 3,
    Run = 4
}
