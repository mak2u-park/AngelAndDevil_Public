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

    // 빔이 거울과 충돌했을 때의 충돌 지점을 구하는 메서드
    private Vector2 collisionPosition(Beam beam)
    {
        // 빔이 거울과 충돌하는 것을 감지하기 위한 레이캐스트
        RaycastHit2D hit = Physics2D.Raycast(beam.transform.position, beam.transform.right, 50f, _mirrorLayerMask);
        return hit.point;
    }

    private void Update()
    {

        if (_reflectedbeam != null && _currentBeam != null)
        {
            Vector2 CollisionPoint = collisionPosition(_currentBeam);
            Vector2 normalVector = (Vector2.up + Vector2.left).normalized; // 거울의 법선 벡터
            Vector2 reflectedDirection = Vector2.Reflect(_currentBeam.transform.right, normalVector); // 반사된 방향

            _reflectedbeam.transform.position = CollisionPoint;    // 충돌 지점으로 이동
            CollisionPoint.y += 0.1f;
            _reflectedbeam.transform.right = reflectedDirection;   // 반사된 방향으로 회전

            Vector2 newscale = _reflectedbeam.transform.localScale;
            newscale.x = _maxLaserRange;                           // 반사된 빔의 길이 설정
            _reflectedbeam.transform.localScale = newscale;

            // 빔이 Ground와 Mirror에 닿는지 확인하기 위한 레이캐스트
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

                Vector2 collisionPoint = collisionPosition(beam);        // 충돌 지점
                Vector2 normalVector = (Vector2.up + Vector2.left).normalized; // 거울의 법선 벡터
                Vector2 reflectedDirection = Vector2.Reflect(beam.transform.right, normalVector); // 반사된 방향

                _reflectedbeam = Instantiate(_beamPrefab, collisionPoint, Quaternion.identity); // 빔 인스턴스 생성
                _reflectedbeam.transform.right = reflectedDirection; // 반사된 방향으로 회전
                Vector2 newscale = _reflectedbeam.transform.localScale;
                newscale.x = _maxLaserRange; // 반사된 빔의 길이 설정
                _reflectedbeam.transform.localScale = newscale; // 빔의 길이 설정


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
