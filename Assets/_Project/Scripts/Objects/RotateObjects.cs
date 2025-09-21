using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1.0f;
    private Transform _objTransform;
    void Start()
    {
        _objTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _objTransform.Rotate(0, _rotationSpeed * Time.deltaTime / 0.01f, 0, Space.Self );
    }
}
