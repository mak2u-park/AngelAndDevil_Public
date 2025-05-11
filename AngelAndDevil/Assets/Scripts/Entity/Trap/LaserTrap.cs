using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] private float _maxLaserRange = 20f;

    [SerializeField] private GameObject _beamPrefab;

    [SerializeField] private GameObject _beamPivot;

    [SerializeField] private LayerMask _levelLayerMask;
    private float _laserRange = 0f;

    private Transform _beamPivotTransform;

    public void Start()
    {
        _laserRange = _maxLaserRange;
        _beamPivotTransform = _beamPivot.transform;

        _beamPrefab = Instantiate(_beamPrefab, _beamPivotTransform.position, transform.rotation);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_beamPivotTransform.position, transform.right, _maxLaserRange, _levelLayerMask);

        if(hit.collider != null && _levelLayerMask.value == (_levelLayerMask.value | (1 << hit.collider.gameObject.layer)))
        {
            _laserRange = hit.distance;
            Debug.DrawRay(_beamPivotTransform.position, transform.right * _laserRange, Color.red);

            Vector3 newScale = _beamPrefab.transform.localScale;
            newScale.x = _laserRange;
            _beamPrefab.transform.localScale = newScale;
            _beamPrefab.transform.rotation = transform.rotation;
            
        }
        else
        {
            Debug.DrawRay(_beamPivotTransform.position, transform.right * _maxLaserRange, Color.green);
            Vector3 newScale = _beamPrefab.transform.localScale;
            newScale.x = _maxLaserRange;
            _beamPrefab.transform.localScale = newScale;
        }
    }

}
