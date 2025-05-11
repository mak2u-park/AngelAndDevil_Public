// Trap.cs
using UnityEngine;
using System.Collections;
public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private TrapType _type;
    [SerializeField] private GameObject[] _projectiles;
    [SerializeField] private float _shootInterval;
    [SerializeField] private float _moveDistance;
    [SerializeField] private int _projectileIndex = 0;
    private float _position;

    private float _shootTime;

    private int _direction = 1;

    public void Start()
    {
        _position = transform.position.y;
    }

    private void Update()
    {
        _shootTime += Time.deltaTime;
        if(_shootTime >= _shootInterval)
        {
            GameObject projectile = Instantiate(_projectiles[_projectileIndex], transform.position, Quaternion.identity);
            _shootTime = 0;
        }
    }

    private void FixedUpdate()
    {
        if(_moveDistance == 0) return;
        
        transform.position += transform.up * Time.fixedDeltaTime * _direction;
        
        if(transform.position.y >= _position + _moveDistance)
        {
            _direction = -1;
        }
        else if(transform.position.y <= _position - _moveDistance)
        {
            _direction = 1;
        }
    }
}