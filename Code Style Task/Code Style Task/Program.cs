using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _fireRate = 1f;

    private void Start()
    {
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
        Vector3 shootDirection = (_target.position - transform.position).normalized;
        GameObject bullet = Instantiate(
            _bulletPrefab,
            transform.position + shootDirection,
            Quaternion.identity
        );

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = shootDirection * _bulletSpeed;
        bullet.transform.up = shootDirection;
    }
}