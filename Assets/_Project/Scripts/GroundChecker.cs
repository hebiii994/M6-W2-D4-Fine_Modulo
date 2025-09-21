using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _coyoteTimeDuration = 0.15f;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private CapsuleCollider _playerCollider;
    [SerializeField][Range(0f, 90f)] private float _maxSlopeAngle = 45f;

    private float _coyoteTimeCounter;
    private bool _isPhysicallyGrounded;
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _playerCollider = GetComponent<CapsuleCollider>();
        if (_playerCollider == null)
        {
            Debug.Log("Inserire CapsuleCollider al componente del check");
        }
        
    }
    private void Update()
    {
        if (_isPhysicallyGrounded)
        {
            _coyoteTimeCounter = _coyoteTimeDuration;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
        IsGrounded = _coyoteTimeCounter > 0f;
    }
    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        float sphereRadius = _playerCollider.radius * 0.9f;
        float checkDistance = (_playerCollider.height / 2) - sphereRadius + 0.1f;
        if (Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out RaycastHit hit, checkDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Crate"))
            {
                hit.collider.GetComponent<Crate>()?.DestroyCrate();
                GetComponent<PlayerController>()?.BounceOnCrate();
                _isPhysicallyGrounded = false;
                return;
            }
            if (((1 << hit.collider.gameObject.layer) & _groundLayer) != 0)
            {
                float slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
                _isPhysicallyGrounded = slopeAngle <= _maxSlopeAngle;
            }
            else
            {
                _isPhysicallyGrounded = false;
            } 
        }
        else
        {
            
            _isPhysicallyGrounded = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (_playerCollider == null) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;

        float checkDistance = (_playerCollider.height / 2) - _playerCollider.radius;
        Vector3 spherePosition = transform.position + _playerCollider.center + Vector3.down * checkDistance;

        // Disegniamo il gizmo usando il raggio del collider
        Gizmos.DrawWireSphere(spherePosition, _playerCollider.radius);
    }




}
