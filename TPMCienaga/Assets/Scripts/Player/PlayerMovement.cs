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
    public CrouchHandler crouchHandler;
    public HeadBob headBob;

    public AudioSource footstepSource; // ← AudioSource en el Player
    public AudioClip footstepClip;     // ← Audio de pasos

    private Vector3 velocity;
    private bool isFootstepPlaying = false;

    public bool canMove = true;

    private void Update()
    {
        if (!canMove)
            return;

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

        // Footstep sound
        HandleFootstepSound(isMoving, isGrounded);
    }

    private void HandleFootstepSound(bool isMoving, bool isGrounded)
    {
        if (isMoving && isGrounded && !isFootstepPlaying)
        {
            footstepSource.clip = footstepClip;
            footstepSource.loop = true;
            footstepSource.Play();
            isFootstepPlaying = true;
        }
        else if ((!isMoving || !isGrounded) && isFootstepPlaying)
        {
            footstepSource.Stop();
            isFootstepPlaying = false;
        }
    }

    private AudioClip originalFootstepClip;


    private void Start()
    {
        originalFootstepClip = footstepClip;
    }

    public void SetTemporaryFootstep(AudioClip newClip)
    {
        if (footstepClip != newClip)
        {
            footstepClip = newClip;
            if (isFootstepPlaying)
            {
                footstepSource.Stop();
                footstepSource.clip = footstepClip;
                footstepSource.Play();
            }
        }
    }

    public void ResetFootstep()
    {
        if (footstepClip != originalFootstepClip)
        {
            footstepClip = originalFootstepClip;
            if (isFootstepPlaying)
            {
                footstepSource.Stop();
                footstepSource.clip = footstepClip;
                footstepSource.Play();
            }
        }
    }


}
