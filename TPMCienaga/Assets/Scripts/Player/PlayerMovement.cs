using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float sprintMultiplier = 2f;
    public float gravity = -9.8f;

    public CharacterController controller;
    public PlayerInputManager inputManager;
    public PlayerCameraController cameraController;
    public StaminaSystem staminaSystem;
    public FootstepAudio footstepAudio;
    public CrouchHandler crouchHandler;
    public HeadBob headBob;

    public bool canMove = true; // ✅ Permite habilitar/deshabilitar movimiento

    private Vector3 velocity;

    private void Update()
    {
        // Permitir mirar siempre con la cámara
        cameraController.HandleLook(inputManager.LookInput);

        if (!canMove)
        {
            // Si no se puede mover, salir sin mover al personaje pero permitir cámara
            return;
        }

        // Manejo de estamina
        bool isRunning = inputManager.Sprinting && inputManager.MoveInput.magnitude > 0.1f && !crouchHandler.IsCrouching;
        staminaSystem.HandleStamina(isRunning);

        // Agacharse
        if (inputManager.CrouchingPressed)
            crouchHandler.ToggleCrouch();

        // Obtener dirección de movimiento horizontal según cámara
        Vector2 moveInput = inputManager.MoveInput;

        Vector3 camForward = cameraController.cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraController.cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDirection = camRight * moveInput.x + camForward * moveInput.y;

        // Calcular velocidad
        float currentSpeed = movementSpeed;
        if (inputManager.Sprinting && staminaSystem.CanSprint && !crouchHandler.IsCrouching)
            currentSpeed *= sprintMultiplier;
        else if (crouchHandler.IsCrouching)
            currentSpeed = movementSpeed / 2f;

        // Aplicar movimiento horizontal
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // HeadBob visual
        bool isMoving = moveInput.magnitude > 0.1f;
        bool isGrounded = controller.isGrounded;
        bool isSprinting = inputManager.Sprinting && isMoving && !crouchHandler.IsCrouching;

        headBob.HandleHeadBob(moveInput, isGrounded, isSprinting);
    }
}
