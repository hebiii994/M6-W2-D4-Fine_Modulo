using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public enum TurretType { Homing, Spreadshot, Alternating }

    [SerializeField] private TurretType _turretType;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 1f;

    [SerializeField] private float _rotationSpeed = 5f;
    private Transform _playerTarget;

    [SerializeField] private int _projectileCount = 3;
    [SerializeField] private float _spreadAngle = 15f;

    [SerializeField] private float _alternatingAngle = 20f;
    private bool shootLeft = true;

    private float _fireTimer;

    private void Update()
    {
        _fireTimer -= Time.deltaTime;

        switch (_turretType)
        {
            case TurretType.Homing:
                HomingUpdate();
                break;
            case TurretType.Spreadshot:
                SpreadshotUpdate();
                break;
            case TurretType.Alternating:
                AlternatingUpdate();
                break;
        }
    }
    private void HomingUpdate()
    {
        if (_playerTarget == null) return;
        Vector3 targetPosition = _playerTarget.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - _firePoint.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        if (_fireTimer <= 0)
        {
            FireProjectile(_firePoint.rotation);
            _fireTimer = 1f / _fireRate;
        }

    }

    private void SpreadshotUpdate()
    {
        if (_fireTimer <= 0)
        {
            float angleStep = _spreadAngle / (_projectileCount - 1);
            float startingAngle = -_spreadAngle / 2;

            for (int i = 0; i < _projectileCount; i++)
            {
                float currentAngle = startingAngle + angleStep * i;
                Quaternion rotation = _firePoint.rotation * Quaternion.Euler(0, currentAngle, 0);
                FireProjectile(rotation);
            }
            _fireTimer = 1f / _fireRate;
        }
    }

    private void AlternatingUpdate()
    {
        if (_fireTimer <= 0)
        {
            float angle = shootLeft ? -_alternatingAngle : _alternatingAngle;
            Quaternion rotation = _firePoint.rotation * Quaternion.Euler(0, angle, 0);
            FireProjectile(rotation);

            shootLeft = !shootLeft; 
            _fireTimer = 1f / _fireRate;
        }
    }
    private void FireProjectile(Quaternion rotation)
    {
        GameObject projectile = ObjectPooler.Instance.SpawnFromPool("Projectile", _firePoint.position, _firePoint.rotation);

        if (projectile != null)
        {
            projectile.transform.position = _firePoint.position;
            projectile.transform.rotation = rotation;
            projectile.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_turretType == TurretType.Homing && other.CompareTag("Player"))
        {
            Transform targetPoint = other.transform.Find("AimTarget");
            _playerTarget = targetPoint != null ? targetPoint : other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_turretType == TurretType.Homing && other.CompareTag("Player"))
        {
            _playerTarget = null;
        }
    }
}
