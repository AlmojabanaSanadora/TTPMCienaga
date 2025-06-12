using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstpersonController : MonoBehaviour
{
    public float movementSpeed = 2;
    public float gravity = -9.8f;
    public Transform cameraTransform;
    public float sensitivity = 0.5f;
    public float minLimit = -80f;
    public float maxLimit = 80f;

    private PlayerInput _inputAction;
    private CharacterController _characaterController;
    private Vector2 _movement;
    private Vector2 _velocity;
    private Vector2 _look;

    private float _currentRotationY;

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

        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetMovement(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        _look = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        Movement();
        Look();
    }

    private void Look()
    {
        Vector2 mouseDelta = _look * sensitivity;

        _currentRotationY = Mathf.Clamp(_currentRotationY - mouseDelta.y, minLimit, maxLimit);
        cameraTransform.localRotation = Quaternion.Euler(_currentRotationY, 0f, 0f);

        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    private void Movement()
    {
        Vector3 move = transform.right * _movement.x + transform.forward * _movement.y;
        _characaterController.Move(move * movementSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        _characaterController.Move(_velocity * Time.deltaTime);
    }
}
