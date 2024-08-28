using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _gravity;

    private Vector3 _moveDir;

    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Transform _transform;

    //private bool _hasJumped;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _jumpAction = _playerInput.actions.FindAction("Jump");
    }

    void Update()
    {
        // Direction
        Vector2 direction = _moveSpeed * _moveAction.ReadValue<Vector2>();
        _moveDir = new Vector3(direction.x, _moveDir.y, direction.y);

        if (_characterController.isGrounded)
        {
            // Jump
            if (_jumpAction.IsPressed())
            {
                _moveDir.y = _jumpForce;
            }
            // Fix weird jump force
            else if (Mathf.Abs(_moveDir.y) > 0.4f)
            {
                _moveDir.y = 0f;
            }
        }
        else
        {
            // Gravity
            _moveDir.y += _gravity * Time.deltaTime;
        }

        //print("Grounded => " + _characterController.isGrounded + " " + _moveDir);
        // Apply
        _characterController.Move(_moveDir * Time.deltaTime);
    }
}
