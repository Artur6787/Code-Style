using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _fireRate = 1f;

    private WaitForSeconds _fireDelay;

    private void Start()
    {
        if (_bulletPrefab == null)
        {
            enabled = false;
            return;
        }

        _fireDelay = new WaitForSeconds(_fireRate);
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (enabled)
        {
            FireBullet();
            yield return _fireDelay;
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