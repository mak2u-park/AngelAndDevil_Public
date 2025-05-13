using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WindTrap : MonoBehaviour
{
    [SerializeField] private float _windPower;

    [SerializeField] private Lever _lever;

    [SerializeField] private LayerMask _targetLayer;

    private Animator _animator;

    private float _applyForce = 0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _applyForce = _windPower;

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(_targetLayer == (_targetLayer | (1 << other.gameObject.layer)))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.AddForce(transform.up * _applyForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody2D가 없습니다.");
            }
        }
    }

    private void FixedUpdate()
    {
        if(_lever == null) return;

        if(_lever.IsEnable)
        {
            _applyForce = 0f;
            _animator.SetBool("Disable", true); 
        }
        else
        {
            _applyForce = _windPower;
            _animator.SetBool("Disable", false);
        }
    }
}
