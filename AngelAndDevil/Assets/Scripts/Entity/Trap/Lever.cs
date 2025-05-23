using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool isEnable;
    private bool isContact = false;

    private const float AngleThreshold = 45f; // 레버 활성화 임계 각도
    private const float LerpSpeed = 2f;      // 레버 회전 보간 속도

    // Gate에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => isEnable;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Lever의 회전축을 설정
        _rigidbody2D.centerOfMass = new Vector2(0f, -0.25f);

        // 오브젝트 이름이 Lever_Top인지 확인
        if (gameObject.name != "Lever1_Top" && gameObject.name != "Lever2_Top")
        {
            Debug.LogError("잘못된 오브젝트입니다 - Lever_Top 전용");
            return;
        }
    }

    // 물리연산이기에 FixedUpdate에서 사용
    private void FixedUpdate()
    {
        float rawZ = _rigidbody2D.transform.localEulerAngles.z;
        float angleZ = (rawZ > 180f) ? rawZ - 360f : rawZ;
        float clampedZ;

        if ( gameObject.name == "Lever1_Top")
        {
             clampedZ = Mathf.Clamp(angleZ, -90f, 0f);
            
            // 레버가 한쪽으로 기울어졌을때 중력의 영향을 받는 것처럼 운동
            if (clampedZ < -AngleThreshold)
            {
                clampedZ = Mathf.Lerp(clampedZ, -90f, Time.fixedDeltaTime * LerpSpeed); // 부드럽게 회전
            }
            else if (clampedZ > -AngleThreshold)
            {
                clampedZ = Mathf.Lerp(clampedZ, 0f, Time.fixedDeltaTime * LerpSpeed); // 부드럽게 회전
            }
            else
            {
                // 정확히 45도일때는 아무일도 일어나지 않음
            }

            // 질량 중심(centerOfMass) 위치를 기준으로 회전
            _rigidbody2D.MoveRotation(clampedZ); 
        }
        else
        {
            clampedZ = Mathf.Clamp(angleZ, 0f, 90f);

            // 레버가 한쪽으로 기울어졌을때 중력의 영향을 받는 것처럼 운동
            if (clampedZ < AngleThreshold)
            {
                clampedZ = Mathf.Lerp(clampedZ, 0f, Time.fixedDeltaTime * LerpSpeed); // 부드럽게 회전
            }
            else if (clampedZ > AngleThreshold)
            {
                clampedZ = Mathf.Lerp(clampedZ, 90f, Time.fixedDeltaTime * LerpSpeed); // 부드럽게 회전
            }
            else
            {
                // 정확히 45도일때는 아무일도 일어나지 않음
            }

            // 질량 중심(centerOfMass) 위치를 기준으로 회전
            _rigidbody2D.MoveRotation(clampedZ); 
        }
        

        // 레버의 회전 각도를 절대값으로 변환
        float clampedZAbs = Mathf.Abs(clampedZ);

        if (!isContact) return;

        if (clampedZAbs > AngleThreshold)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    public void Enable()
    {
        if(isEnable == false)
        {
            SoundManager.Instance.PlaySFX("Lever");
        }
        isEnable = true;
        // 추가 활성화 로직
    }

    public void Disable()
    {
        if(isEnable == true)
        {
            SoundManager.Instance.PlaySFX("Lever");
        }
        isEnable = false;
        // 추가 비활성화 로직
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = false;
        }
    }

}
