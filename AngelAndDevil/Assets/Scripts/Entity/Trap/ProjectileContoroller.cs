// Trap.cs
using UnityEngine;
using System.Collections;
public class ProjectileContoroller : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;

    [SerializeField] private LayerMask _levelColliderLayer;

    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rigidbody;
    public GameObject _pivot;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootProjectile());
        _pivot = transform.GetChild(0).gameObject;
    }

    public void Init(bool isRight)
    {
        transform.localRotation = Quaternion.Euler(0, 0, isRight ? 0 : 180);
    }

    private IEnumerator ShootProjectile()
    { 
        Vector2 direction = transform.right;
        _rigidbody.velocity = direction * _speed;

        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_levelColliderLayer.value == (_levelColliderLayer.value | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
        if(_targetLayer.value == (_targetLayer.value | (1 << other.gameObject.layer)))
        {
            other.gameObject.GetComponent<PlayerController>().Die();
            Destroy(gameObject);
        }
    }
}