using UnityEngine;
using System.Collections;

public class MovingWallPlatform : MonoBehaviour
{
    private enum WallType
    {
        STATIC, // Non si muove
        DYNAMIC // Si muove in un ciclo continuo
    }

    [Header("Behavior")]
    [SerializeField] private WallType _wallType = WallType.DYNAMIC;
    [Tooltip("Delay before the wall starts moving the first time.")]
    [SerializeField] private float _initialDelay = 0f;
    [Tooltip("Delay after the wall reaches the end before returning.")]
    [SerializeField] private float _pauseAtEndPoint = 2f;
    [Tooltip("Delay after the wall returns to the start before pushing again.")]
    [SerializeField] private float _pauseAtStartPoint = 2f;


    [Header("Movement")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed = 2f;

    private void Start()
    {

        transform.position = _startPoint.position;

        if (_wallType == WallType.DYNAMIC)
        {
            StartCoroutine(MovementLoop());
        }
    }

    private IEnumerator MovementLoop()
    {
        yield return new WaitForSeconds(_initialDelay);

        while (true) 
        {
            yield return StartCoroutine(MoveTo(_endPoint.position));

            yield return new WaitForSeconds(_pauseAtEndPoint);

            yield return StartCoroutine(MoveTo(_startPoint.position));

            yield return new WaitForSeconds(_pauseAtStartPoint);

        }
    }

    private IEnumerator MoveTo(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
            yield return null;
        }
        transform.position = destination;
    }
}