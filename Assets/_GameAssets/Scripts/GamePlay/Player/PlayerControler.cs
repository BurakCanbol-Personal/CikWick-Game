using System;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action<PlayerState> OnPlayerStateChange;

    [Header("References")]
    [SerializeField] private Transform _orientationTransform;



    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;



    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCoolDown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _slidingCoolDown;



    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;



    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;



    private StateController _stateController;
    private Rigidbody _playerRigidbody;
    private float _startingMovementSpeed, _startingJumpingForce;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

        _startingMovementSpeed = _movementSpeed;
        _startingJumpingForce = _jumpForce;

    }


    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume) { return; }

        SetInput();
        SetStates();
        setPlayerDrag();
        LimitPLayerSpeed();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume) { return; }

        SetPlayerMovement();
    }


    private void SetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;

        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;

        }
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            // Ziplama islemni gerceklesecek
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCoolDown);
            AudioManager.Instance.Play(SoundType.JumpSound);

        }
    }



    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
            OnPlayerStateChange?.Invoke(newState);
        }
    }


    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput
         + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f

        };

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }

    private void setPlayerDrag()
    {

        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Slide => _slideDrag,
            PlayerState.Move => _groundDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
        };

    }

    private void LimitPLayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void CoolDownPlayerSpeed()
    {
        _isSliding = false;
    }

    private void SetPlayerJumping()
    {
        OnPlayerJumped?.Invoke();

        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    #region Helper Functions

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return _isSliding;
    }

    public void SetMovementSpeed(float speed, float duration)
    {
        _movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);

    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = _startingMovementSpeed;
    }

    public void SetJumpingForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke(nameof(ResetJumpingForce), duration);
    }

    private void ResetJumpingForce()
    {
        _jumpForce = _startingJumpingForce;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return _playerRigidbody;
    }

    public bool CanCatChase()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _playerHeight * 0.5f + 02f, _groundLayer))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Consts.Layers.FLOOR_LAYER))
            {
                return true;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Consts.Layers.GROUND_LAYER))
            {
                return false;
            }
        }
        return false;
    }

    #endregion

}
