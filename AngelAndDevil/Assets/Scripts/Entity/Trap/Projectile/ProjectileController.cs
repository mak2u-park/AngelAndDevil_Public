// Trap.cs
using UnityEngine;
using System.Collections;
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;

    [SerializeField] private LayerMask _levelColliderLayer;

    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rigidbody;

	private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(bool isRight)
    {
        if(_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        transform.localRotation = Quaternion.Euler(0, isRight ? 0 : 180, 0);
        StartCoroutine(ShootProjectile());

    }
    private IEnumerator ShootProjectile()
    { 
        Vector2 direction = transform.right;
        _rigidbody.velocity = direction * _speed;

        yield return new WaitForSeconds(_duration);
        ProjectilePool.Instance.ReturnProjectile(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_levelColliderLayer.value == (_levelColliderLayer.value | (1 << other.gameObject.layer)))
        {
            ProjectilePool.Instance.ReturnProjectile(this);
        }
        if(_targetLayer.value == (_targetLayer.value | (1 << other.gameObject.layer)))
        {
            other.gameObject.GetComponent<PlayerController>().Die();
            ProjectilePool.Instance.ReturnProjectile(this);
        }
    }
}