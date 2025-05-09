using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool isEnable;
    private bool isContact = false;

    // TrapDoor에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => isEnable;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Lever의 회전축을 설정
        _rigidbody2D.centerOfMass = new Vector2(0f, -0.25f);
    }

    // 물리연산이기에 FixedUpdate에서 사용
    void FixedUpdate()
    {
        float rawZ = _rigidbody2D.transform.localEulerAngles.z;
        float angleZ = (rawZ > 180f) ? rawZ - 360f : rawZ;
        float clampedZ;

        if ( gameObject.name == "Lever1_Top")
        {
             clampedZ = Mathf.Clamp(angleZ, -90f, 0f);
            _rigidbody2D.MoveRotation(clampedZ); // 질량 중심(centerOfMass) 위치를 기준으로 회전
        }
        else if (gameObject.name == "Lever2_Top")
        {
            clampedZ = Mathf.Clamp(angleZ, 0f, 90f);
            _rigidbody2D.MoveRotation(clampedZ); // 질량 중심(centerOfMass) 위치를 기준으로 회전
        }
        else
        {
            Debug.LogError("레버 이름 오류");
            return;
        }

        // 레버의 회전 각도를 절대값으로 변환
        float clampedZabs = Mathf.Abs(clampedZ);

        if (!isContact) return;

        if (clampedZabs > 45f)
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
        isEnable = true;
        // 추가 활성화 로직
        Debug.Log("레버 활성화");
    }

    public void Disable()
    {
        isEnable = false;
        // 추가 비활성화 로직
        Debug.Log("레버 비활성화");
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
