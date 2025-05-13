using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private GameObject _beamPrefab;

    [SerializeField] private float _maxLaserRange = 20f;

    [Header("Laser")]
    [SerializeField] private LayerMask _laserLayerMask;

    [Header("Mirror")]
    [SerializeField] private LayerMask _mirrorLayerMask;

    [Header("Ground, Mirror")]
    [SerializeField] private LayerMask _groundLayerMask;

    private GameObject _reflectedbeam = null;
    private Beam _currentBeam = null;
    private float _laserRange;

    // ���� �ſ�� �浹���� ���� �浹 ������ ���ϴ� �޼���
    private Vector2 collisionPosition(Beam beam)
    {
        // ���� �ſ�� �浹�ϴ� ���� �����ϱ� ���� ����ĳ��Ʈ
        RaycastHit2D hit = Physics2D.Raycast(beam.transform.position, beam.transform.right, 50f, _mirrorLayerMask);
        return hit.point;
    }

    private void Update()
    {

        if (_reflectedbeam != null && _currentBeam != null)
        {
            Vector2 CollisionPoint = collisionPosition(_currentBeam);
            Vector2 normalVector = (Vector2.up + Vector2.left).normalized; // �ſ��� ���� ����
            Vector2 reflectedDirection = Vector2.Reflect(_currentBeam.transform.right, normalVector); // �ݻ�� ����

            _reflectedbeam.transform.position = CollisionPoint;    // �浹 �������� �̵�
            CollisionPoint.y += 0.1f;
            _reflectedbeam.transform.right = reflectedDirection;   // �ݻ�� �������� ȸ��

            Vector2 newscale = _reflectedbeam.transform.localScale;
            newscale.x = _maxLaserRange;                           // �ݻ�� ���� ���� ����
            _reflectedbeam.transform.localScale = newscale;

            // ���� Ground�� Mirror�� ����� Ȯ���ϱ� ���� ����ĳ��Ʈ
            RaycastHit2D hit2 = Physics2D.Raycast(CollisionPoint, reflectedDirection, _maxLaserRange, _groundLayerMask);
            if (hit2.collider != null && _groundLayerMask.value == (_groundLayerMask.value | (1 << hit2.collider.gameObject.layer)))
            {
                _laserRange = hit2.distance;
                Debug.DrawRay(CollisionPoint, reflectedDirection * _laserRange, Color.red);

                Vector3 newScale = _reflectedbeam.transform.localScale;
                newScale.x = _laserRange;
                _reflectedbeam.transform.localScale = newScale;

            }
            else
            {
                _laserRange = _maxLaserRange;
                Debug.DrawRay(CollisionPoint, reflectedDirection * _maxLaserRange, Color.green);
                Vector3 newScale = _beamPrefab.transform.localScale;
                newScale.x = _maxLaserRange;
                _beamPrefab.transform.localScale = newScale;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _laserLayerMask) != 0)
        {
            Beam beam = collision.gameObject.GetComponent<Beam>();
            if (beam != null && _reflectedbeam == null)
            {
                _currentBeam = beam;

                Vector2 collisionPoint = collisionPosition(beam);        // �浹 ����
                Vector2 normalVector = (Vector2.up + Vector2.left).normalized; // �ſ��� ���� ����
                Vector2 reflectedDirection = Vector2.Reflect(beam.transform.right, normalVector); // �ݻ�� ����

                _reflectedbeam = Instantiate(_beamPrefab, collisionPoint, Quaternion.identity); // �� �ν��Ͻ� ����
                _reflectedbeam.transform.right = reflectedDirection; // �ݻ�� �������� ȸ��
                Vector2 newscale = _reflectedbeam.transform.localScale;
                newscale.x = _maxLaserRange; // �ݻ�� ���� ���� ����
                _reflectedbeam.transform.localScale = newscale; // ���� ���� ����


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _laserLayerMask) != 0)
        {
            Beam beam = collision.gameObject.GetComponent<Beam>();
            if (beam != null && beam == _currentBeam)
            {
                Destroy(_reflectedbeam);
                _reflectedbeam = null;
                _currentBeam = null;
            }
        }
    }
}
