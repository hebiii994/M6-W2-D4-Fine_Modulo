using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingPlatform : MonoBehaviour
{
    [SerializeField] private float _maxTiltAngle = 15f;
    [SerializeField] private float _tiltSpeed = 1.5f;

    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialRotation = transform.rotation;
    }
   
    // Update is called once per frame
    void Update()
    {
        float tiltZ = _maxTiltAngle * Mathf.Sin(Time.time * _tiltSpeed);
        transform.localRotation = _initialRotation * Quaternion.Euler(0, 0, tiltZ);
    }
}
