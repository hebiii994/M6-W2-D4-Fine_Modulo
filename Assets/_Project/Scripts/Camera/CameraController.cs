using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -5);
    [SerializeField] private float _mouseSensitivity = 3f;
    [SerializeField] private float _controllerSensitivity = 3f;
    [SerializeField] private float _bottomClamp = -30f;
    [SerializeField] private float _topClamp = 60f;
    [SerializeField] private float _lookAtOffsetY = 2f;

    private float _yaw;
    private float _pitch;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        if (!GameManager.Instance.IsGameActive)
        {
            return;
        }

        if (_target == null) return;
        _yaw += Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        _pitch -= Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _yaw += Input.GetAxis("LookHorizontal") * _controllerSensitivity * Time.deltaTime;
        _pitch += Input.GetAxis("LookVertical") * _controllerSensitivity * Time.deltaTime;



        _pitch = Mathf.Clamp(_pitch, _bottomClamp, _topClamp);
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 desiredPosition = _target.position + rotation * _offset;
        transform.position = desiredPosition;
        transform.LookAt(_target.position + Vector3.up * _lookAtOffsetY);
    }
}
