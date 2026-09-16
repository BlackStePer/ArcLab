using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _playerOwner;

    private void Start()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_bulletPrefab == null || _firePoint == null || _playerOwner == null) return;

        Quaternion bulletRotation = _mainCamera.transform.rotation;
        GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, bulletRotation);

        Collider playerCollider = _playerOwner.GetComponent<Collider>();
        Collider bulletCollider = bulletObj.GetComponent<Collider>();

        if (playerCollider != null && bulletCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }

        if (bulletObj.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Init(_playerOwner, _playerOwner.GetComponent<PlayerView>().BattleManager);

        }
    }
}