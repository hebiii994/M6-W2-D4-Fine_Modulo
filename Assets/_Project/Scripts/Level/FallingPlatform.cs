using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float _fallDelay = 1.5f;
    [SerializeField] private float _respawnDelay = 5f;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private Rigidbody _rb;
    private bool _isPlayerOnPlatform = false;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        _rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isPlayerOnPlatform)
        {
            _isPlayerOnPlatform = true;
            StartCoroutine(FallSequence());
        }
    }

    private IEnumerator FallSequence()
    {
        yield return new WaitForSeconds(_fallDelay);
        _rb.isKinematic = false;
        _rb.useGravity = true;
        yield return new WaitForSeconds(_respawnDelay);
        
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
        _rb.isKinematic = true;
        _isPlayerOnPlatform = false ;
        
    }
}
