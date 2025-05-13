// Trap.cs
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private TurretType _type;
    [SerializeField] private GameObject[] _projectiles;

    [SerializeField] private Sprite[] _renderers;
    [SerializeField] private float _shootInterval;
    [SerializeField] private float _moveDistance;
    [SerializeField] private int _projectileIndex = 0;
    [SerializeField] private bool _isRight;
    [SerializeField] private Plate2[] _plate;

    private SpriteRenderer _renderer;

    private int _currentRendererIndex = 0;

    private float _position;

    private float _shootTime;

    private int _direction = 1;

    public void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        SetType(_type);
        
        if(_plate != null)
        {
            foreach(var plate in _plate)
            {
                plate.AddEnableEvent(ReverseType);
                plate.AddDisableEvent(ReverseType);
            }
        }
    }

    private void Update()
    {
        _shootTime += Time.deltaTime;
        if(_shootTime >= _shootInterval)
        {
            GameObject projectile = Instantiate(_projectiles[_projectileIndex], transform.position, Quaternion.identity);
            ProjectileContoroller projectileContoroller = projectile.GetComponent<ProjectileContoroller>();
            projectileContoroller.Init(_isRight);

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

    public void SetType(TurretType type)
    {
        _position = transform.position.y;
        if(_type == TurretType.Green)
        {
            _projectileIndex = 0;
            _currentRendererIndex = 0;
            
        }
        else if(_type == TurretType.Blue)
        {
            _projectileIndex = 1;
            _currentRendererIndex = 1;
        }
        else if(_type == TurretType.Red)
        {
            _projectileIndex = 2;
            _currentRendererIndex = 2;
        }
        else
        {
            _projectileIndex = 0;
            _currentRendererIndex = 0;
        }
        _renderer.sprite = _renderers[_currentRendererIndex];
    }

    public void ReverseType()
    {
        if(_type == TurretType.Red)
        {
            _type = TurretType.Blue;
            SetType(_type);
        }
        else if(_type == TurretType.Blue)
        {
            _type = TurretType.Red;
            SetType(_type);
        }
    }
}