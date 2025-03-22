using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _fireRate = 1f;

    private void Start()
    {
        if (_bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned!", this);
            enabled = false;
            return;
        }

        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (enabled)
        {
            FireBullet();
            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void FireBullet()
    {
        if (_target == null) return;

        Vector3 shootDirection = (_target.position - transform.position).normalized;
        Bullet bullet = Instantiate(
            _bulletPrefab,
            transform.position + shootDirection,
            Quaternion.identity
        );

        bullet.Initialize(shootDirection, _bulletSpeed);
    }
}