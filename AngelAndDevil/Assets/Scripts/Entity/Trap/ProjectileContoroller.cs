// Trap.cs
using UnityEngine;
using System.Collections;
public class ProjectileContoroller : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;

    [SerializeField] private LayerMask _levelColliderLayer;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile()
    { 
        Vector3 direction = transform.right;
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
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Die();
            Destroy(gameObject);
        }
    }
}