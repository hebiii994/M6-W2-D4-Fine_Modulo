using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorMechanics : MonoBehaviour
{
    [SerializeField] private Transform _doorOpening;
    [SerializeField] private float _targetYRotation = -130f;
    [SerializeField] private float _openSpeed = 2f;

    private bool _isOpen = false;

    public void OpenDoor()
    {
        if (_isOpen) return;
        _isOpen = true;
        StartCoroutine(OpenSequence());
    }

    private IEnumerator OpenSequence()
    {
        Quaternion startRotation = _doorOpening.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, _targetYRotation, 0);
        float journey = 0f;
        while (journey < 1f)
        {
            journey += Time.deltaTime * _openSpeed;
            _doorOpening.transform.rotation = Quaternion.Slerp(startRotation, endRotation, journey);
            yield return null;
        }
        _doorOpening.transform.rotation = endRotation;
        Debug.Log("Porta aperta!");

    }

}
