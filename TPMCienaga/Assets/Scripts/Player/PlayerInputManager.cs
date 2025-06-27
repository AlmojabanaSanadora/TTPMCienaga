using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool Sprinting { get; private set; }
    public bool CrouchingPressed { get; private set; }

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Enable();

        _input.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        _input.Player.Lock.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        _input.Player.Lock.canceled += ctx => LookInput = Vector2.zero;

        _input.Player.Sprint.performed += ctx => Sprinting = true;
        _input.Player.Sprint.canceled += ctx => Sprinting = false;

        _input.Player.Crouch.performed += ctx => CrouchingPressed = true;
    }

    private void LateUpdate()
    {
        CrouchingPressed = false; // Se detecta solo una vez por frame
    }
}