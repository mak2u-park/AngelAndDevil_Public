using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plate2 : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool _isEnable;
    private bool _isContact = false;
    private float _SetY;
    // Elevator에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => _isEnable;

    public event Action OnEnable;
    public event Action OnDisable;

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
            PosY += 0.5f * Time.fixedDeltaTime;
            PosY = Mathf.Min(PosY, _SetY);

            transform.position = new Vector2(transform.position.x, PosY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            _isContact = true;
            Enable();

            float PosY = transform.position.y;
            PosY = Mathf.Clamp(PosY, _SetY - 1f, _SetY);

            transform.position = new Vector2(transform.position.x, PosY);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            _isContact = false;
            Disable();
        }
    }

    private IEnumerator RestorePositionCoroutine()
    {
        float time = 0.2f;
        float posY = transform.position.y;
        if (!_isContact && posY < _SetY)
        {
            posY += 0.5f * time;
            posY = Mathf.Min(posY, _SetY);
            transform.position = new Vector2(transform.position.x, posY);
        }
        yield return new WaitForSeconds(time);
    }
}



