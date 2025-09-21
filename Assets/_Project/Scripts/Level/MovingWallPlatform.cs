using UnityEngine;

public class MovingWallPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody _movingWallRb; 

    [SerializeField] private float _interval = 4f;
    [SerializeField] private float _initialDelay = 2f;

    [SerializeField] private float _pushDistance = 5f;
    [SerializeField] private float _pushSpeed = 10f;
    [SerializeField] private float _returnSpeed = 2f;


    private enum WallState { Idle, Pushing, Returning }
    private WallState _currentState = WallState.Idle;

    private Vector3 _wallStartPosition;
    private Vector3 _wallEndPosition;
    private float timer;

    private void Awake()
    {
        if (_movingWallRb == null)
        {
            Debug.LogError("Il Rigidbody del muro non è stato assegnato!", this);
            return;
        }
        _wallStartPosition = _movingWallRb.position;
        _wallEndPosition = _wallStartPosition + transform.forward * _pushDistance;
        timer = _initialDelay;
    }

    private void Update()
    {
        if (_currentState == WallState.Idle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                _currentState = WallState.Pushing;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == WallState.Pushing)
        {
            Push();
        }
        else if (_currentState == WallState.Returning)
        {
            Return();
        }
    }

    private void Push()
    {

        Vector3 newPosition = Vector3.MoveTowards(_movingWallRb.position, _wallEndPosition, _pushSpeed * Time.fixedDeltaTime);
        _movingWallRb.MovePosition(newPosition);

        if (Vector3.Distance(_movingWallRb.position, _wallEndPosition) < 0.01f)
        {
            _currentState = WallState.Returning;
        }
    }

    private void Return()
    {
        Vector3 newPosition = Vector3.MoveTowards(_movingWallRb.position, _wallStartPosition, _returnSpeed * Time.fixedDeltaTime);
        _movingWallRb.MovePosition(newPosition);

        if (Vector3.Distance(_movingWallRb.position, _wallStartPosition) < 0.01f)
        {
            _currentState = WallState.Idle;
            timer = _interval;
        }
    }
}