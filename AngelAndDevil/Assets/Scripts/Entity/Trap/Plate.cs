using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:AngelAndDevil/Assets/Scripts/Entity/Trap/Button.cs

public class Button : MonoBehaviour, IEnable
=======
public class Plate : MonoBehaviour, IEnable
>>>>>>> 33c33d0cf12aa2d9b9a5fbf8241e4ab96c86cd61:AngelAndDevil/Assets/Scripts/Entity/Trap/Plate.cs
{
    Rigidbody2D _rigidbody2D;
    private bool isEnable;
    private bool isContact = false;
    private float SetY;
    // Elevator에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => isEnable;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SetY = transform.position.y;

        // 오브젝트 이름이 Button_Top인지 확인
        if (gameObject.name != "Button_Top")
        {
            Debug.LogError("잘못된 오브젝트입니다 - Button_Top 전용");
            return;
        }
    }

    void FixedUpdate()
    {
        float PosY = transform.position.y;
        PosY = Mathf.Clamp(PosY, SetY - 1f, SetY);

        if (!isContact && PosY < SetY)
        {
            PosY += 0.5f * Time.fixedDeltaTime;
            PosY = Mathf.Min(PosY, SetY);
            Disable();
        }
        else if (isContact && PosY < SetY - 0.1f)
        {
            Enable();
        }

        transform.position = new Vector2(transform.position.x, PosY);
    }

    public void Enable()
    {
        isEnable = true;
        Debug.Log("버튼 활성화");
    }

    public void Disable()
    {
        isEnable = false;
        Debug.Log("버튼 비활성화");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            isContact = true;
            Debug.Log("버튼 접촉");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            isContact = false;
            Debug.Log("버튼 비접촉");
        }

    }
}



