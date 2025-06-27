using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float sprintMultiplier = 3.5f;
    public float gravity = -9.8f;

    public CharacterController controller;
    public PlayerInputManager inputManager;
    public PlayerCameraController cameraController;
    public StaminaSystem staminaSystem;
    public FootstepAudio footstepAudio;
    public CrouchHandler crouchHandler;
    public HeadBob headBob;

    private Vector3 velocity;

    private void Update()
    {
        staminaSystem.HandleStamina(
            inputManager.Sprinting &&
            inputManager.MoveInput.magnitude > 0.1f &&
            !crouchHandler.IsCrouching
        );

        if (inputManager.CrouchingPressed)
            crouchHandler.ToggleCrouch();

        Vector2 moveInput = inputManager.MoveInput;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float currentSpeed = movementSpeed;
        if (inputManager.Sprinting && staminaSystem.CanSprint && !crouchHandler.IsCrouching)
            currentSpeed *= sprintMultiplier;
        else if (crouchHandler.IsCrouching)
            currentSpeed = movementSpeed / 2f;

        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        cameraController.HandleLook(inputManager.LookInput);

        footstepAudio.HandleFootsteps(
            moveInput.magnitude > 0.1f,
            controller.isGrounded,
            transform.position
        );

        headBob.HandleHeadBob(
            moveInput,
            controller.isGrounded,
            inputManager.Sprinting
        );
    }
}
