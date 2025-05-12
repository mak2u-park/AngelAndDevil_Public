using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, IEnable
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
        Debug.Log("플레이트 활성화");
    }

    public void Disable()
    {
        isEnable = false;
        Debug.Log("플레이트 비활성화");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            isContact = true;
            Debug.Log("플레이트 접촉");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            isContact = false;
            Debug.Log("플레이트 비접촉");
        }

    }
}



