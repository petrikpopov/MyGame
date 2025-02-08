using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
public class CharacterScript : MonoBehaviour
{
     private InputAction moveAction;
    private InputAction jumpAction;
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
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        if(cameraForward != Vector3.zero)
        {
            cameraForward.Normalize();
        }
        Vector3 moveStep = playerSpeed * Time.deltaTime * (
            moveValue.x * Camera.main.transform.right +
            moveValue.y * cameraForward
        );
        characterController.Move(moveStep);
        // Makes the player jump
        if (jumpAction.ReadValue<float>() > 0 && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
