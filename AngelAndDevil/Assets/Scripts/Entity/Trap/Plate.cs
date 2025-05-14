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
    private int _contectCount = 0;

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
        if(isEnable == false)
        {
            SoundManager.Instance.PlaySFX("Plate");
        }
        isEnable = true;
    }

    public void Disable()
    {
        if(isEnable == true)
        {
            SoundManager.Instance.PlaySFX("Plate");
        }
        isEnable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            _contectCount++;
            if(_contectCount == 1)
            {
                isContact = true;
                SoundManager.Instance.PlaySFX("Plate");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Block"))
        {
            _contectCount--;
            if(_contectCount == 0)
            {
                isContact = false;
            }
        }

    }
}



