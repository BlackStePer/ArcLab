using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifeTime = 4f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        Destroy(gameObject, _lifeTime);

        _rb.linearVelocity = transform.forward * _speed;
    }
}