using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _airControllMultiplier;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private float _moveRotationSpeed = 10f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GroundChecker _groundCheck;
    [SerializeField] private Transform _playerVisuals;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private LayerMask _crateLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _bounceForce = 10f;
    private Vector3 _dir;
    private Vector3 _inputVector;
    private bool _canAttack = true;
    private bool _isApplyingAnimationMovement = false;
    private bool _wasGrounded;
    private PlayerAudio _playerAudio;


    private Transform _mainCameraTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundCheck = GetComponent<GroundChecker>();
        _mainCameraTransform = Camera.main.transform;
        _playerAudio = GetComponent<PlayerAudio>();

    }
    private void Start()
    {
        _wasGrounded = _groundCheck.IsGrounded;
    }

    private void Update()
    {
        HandleInput();

        if (Input.GetButtonDown("Jump") && _groundCheck.IsGrounded)
        {
            Jump();
            _animator.SetTrigger("Jump");
        }
        if (Input.GetButtonDown("Fire1") && _canAttack)
        {
            SpinAttack();
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if (_animator == null) return;
        float inputMagnitude = _inputVector.magnitude;
        if (inputMagnitude < 0.1f)
        {
            inputMagnitude = 0f;
        }
        _animator.SetFloat("Speed", inputMagnitude * _speed);
        _animator.SetBool("IsGrounded", _groundCheck.IsGrounded);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        CheckLanding();
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _inputVector = new Vector3(x, 0f, y);

        if (_inputVector.magnitude > 1f)
        {
            _inputVector.Normalize();
        }

        Vector3 forward = _mainCameraTransform.forward;
        Vector3 right = _mainCameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        _dir = (forward * _inputVector.z) + (right * _inputVector.x);
    }

    private void HandleMovement()
    {
        float currentSpeed = _groundCheck.IsGrounded ? _speed : _speed * _airControllMultiplier;

        Vector3 movement = _dir * currentSpeed;
        movement.y = _rb.velocity.y;
        _rb.velocity = movement;
    }

    private void Jump()
    {
        _playerAudio?.PlayJumpSound();
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }
    private void HandleRotation()
    {
        if (_dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_dir, Vector3.up);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _moveRotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void SpinAttack()
    {
        _isApplyingAnimationMovement = true;
        _animator.SetTrigger("Spin");
        _playerAudio?.PlaySpinSound();
        StartCoroutine(SpinCooldownCoroutine());
        Collider[] hits = Physics.OverlapSphere(transform.position, _attackRange, _crateLayer);
        foreach (Collider hit in hits)
        {
            hit.GetComponent<Crate>()?.DestroyCrate();
        }
    }
    public void OnSpinAnimationComplete()
    {
        _isApplyingAnimationMovement = false;
    }
    private void OnAnimatorMove()
    {
        if (GameManager.Instance.IsGameActive && _isApplyingAnimationMovement)
        {
            _rb.position += _animator.deltaPosition;
            _rb.rotation *= _animator.deltaRotation;
        }
    }

    private IEnumerator SpinCooldownCoroutine()
    {
        _canAttack = false;
        float spinDuration = 0.3f;
        float elapsedTime = 0f;
        float spinAnimationSpeed = 1080f;

        while (elapsedTime < spinDuration)
        {

            _playerVisuals.Rotate(Vector3.up, spinAnimationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _playerVisuals.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;

    }

    public void BounceOnCrate()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(Vector3.up * _bounceForce, ForceMode.Impulse);
        _animator?.SetTrigger("Jump");
    }

    private void CheckLanding()
    {
        if (!_wasGrounded && _groundCheck.IsGrounded)
        {
            _playerAudio?.PlayLandingSound();
        }
        _wasGrounded = _groundCheck.IsGrounded;
    }
}
