using UnityEngine;
using UnityEngine.InputSystem;

public class FirstpersonController : MonoBehaviour
{
    [Header("Movimiento")]
    public float movementSpeed = 2f;
    public float sprintMultiplier = 1.5f;
    public float gravity = -9.8f;

    [Header("Cámara")]
    public Transform cameraTransform;
    public float sensitivity = 0.5f;
    public float minLimit = -80f;
    public float maxLimit = 80f;

    [Header("Head Bob")]
    public float bobFrequency = 10f;
    public float bobAmplitude = 0.05f;

    private PlayerInput _inputAction;
    private CharacterController _characaterController;
    private Vector2 _movement;
    private Vector2 _velocity;
    private Vector2 _look;

    private float _currentRotationY;
    private bool _isSprinting;

    private Vector3 _initialCameraLocalPos;
    private float _bobTimer;

    private void Awake()
    {
        _inputAction = new PlayerInput();
        _characaterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _inputAction.Player.Enable();

        _inputAction.Player.Move.performed += SetMovement;
        _inputAction.Player.Move.canceled += ctx => _movement = Vector2.zero;

        _inputAction.Player.Lock.performed += SetLook;
        _inputAction.Player.Lock.canceled += ctx => _look = Vector2.zero;

        _inputAction.Player.Sprint.performed += ctx => _isSprinting = true;
        _inputAction.Player.Sprint.canceled += ctx => _isSprinting = false;

        _initialCameraLocalPos = cameraTransform.localPosition;

        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Movement();
        Look();
        HeadBob();
    }

    private void SetLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }

    private void SetMovement(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void Look()
    {
        Vector2 mouseDelta = _look * sensitivity;

        _currentRotationY = Mathf.Clamp(_currentRotationY - mouseDelta.y, minLimit, maxLimit);
        cameraTransform.localRotation = Quaternion.Euler(_currentRotationY, 0, 0);

        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    private void Movement()
    {
        float currentSpeed = _isSprinting ? movementSpeed * sprintMultiplier : movementSpeed;

        Vector3 move = transform.right * _movement.x + transform.forward * _movement.y;
        _characaterController.Move(move * currentSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        _characaterController.Move(_velocity * Time.deltaTime);
    }

    private void HeadBob()
    {
        if (_movement.magnitude > 0.1f && _characaterController.isGrounded)
        {
            _bobTimer += Time.deltaTime * bobFrequency * (_isSprinting ? 1.5f : 1f);
            float offsetY = Mathf.Sin(_bobTimer) * bobAmplitude;
            cameraTransform.localPosition = _initialCameraLocalPos + new Vector3(0, offsetY, 0);
        }
        else
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _initialCameraLocalPos, Time.deltaTime * 5f);
            _bobTimer = 0f;
        }
    }
}
