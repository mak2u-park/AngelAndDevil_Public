using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReversePlate : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool _isEnable;
    private bool _isContact = false;
    private float _SetY;
    // Elevator에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => _isEnable;

    public event Action OnEnable;
    public event Action OnDisable;

    private int _contectCount = 0;

    private const float ReversePlateRiseSpeed = 0.5f;

    private const float ReversePlateLimitY = 1f;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _SetY = transform.position.y;
    }
    public void AddEnableEvent(Action action)
    {
        OnEnable += action;
    }

    public void AddDisableEvent(Action action)
    {
        OnDisable += action;
    }

    public void RemoveEnableEvent(Action action)
    {
        OnEnable -= action;
    }

    public void RemoveDisableEvent(Action action)
    {
        OnDisable -= action;
    }

    public void Enable()
    {
        _isEnable = true;
        OnEnable?.Invoke();
    }

    public void Disable()
    {
        _isEnable = false;
        OnDisable?.Invoke();
    }

    public void FixedUpdate()
    {
        float PosY = transform.position.y;
        if (!_isContact && PosY < _SetY)
        {
            PosY += ReversePlateRiseSpeed * Time.fixedDeltaTime;
            PosY = Mathf.Min(PosY, _SetY);

            transform.position = new Vector2(transform.position.x, PosY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            _contectCount++;
            if(_contectCount == 1)
            {
                _isContact = true;
                Enable();

            float PosY = transform.position.y;
            PosY = Mathf.Clamp(PosY, _SetY - ReversePlateLimitY, _SetY);

            transform.position = new Vector2(transform.position.x, PosY);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            _contectCount--;
            if(_contectCount == 0)
            {
                _isContact = false;
                Disable();
            }
        }
    }
}



