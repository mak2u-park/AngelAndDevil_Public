using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonStone : MonoBehaviour
{
    [SerializeField] LaserDetector laserDetector;

    private Collider2D _collider;
    private Renderer _renderer;

    private void Awake()
    {
        _collider = GetComponentInChildren<Collider2D>();
        _renderer = GetComponentInChildren<Renderer>();
        
    }

    private void Update()
    {
        if (laserDetector != null)
        {
            _collider.enabled = laserDetector.IsEnable;
            _renderer.enabled = laserDetector.IsEnable;
        }
    }
}
