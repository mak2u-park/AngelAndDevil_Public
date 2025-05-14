using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool _isEnable;
    private bool isContact = false;
    private float SetY;
    // Elevator에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => _isEnable;
    private int _contectCount = 0;

    private const float PlateRiseSpeed = 0.5f;
    private const float PlateLimitY = 1f;
    private const float EnableY = 0.1f;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SetY = transform.position.y;
    }

    void FixedUpdate()
    {
        float PosY = transform.position.y;
        PosY = Mathf.Clamp(PosY, SetY - PlateLimitY, SetY);

        if (!isContact && PosY < SetY)
        {
            PosY += PlateRiseSpeed * Time.fixedDeltaTime;
            PosY = Mathf.Min(PosY, SetY);
            Disable();
        }
        else if (isContact && PosY < SetY - EnableY)
        {
            Enable();
        }

        transform.position = new Vector2(transform.position.x, PosY);
    }

    public void Enable()
    {
        if(_isEnable == false)
        {
            SoundManager.Instance.PlaySFX("Plate");
        }
        _isEnable = true;
    }

    public void Disable()
    {
        if(_isEnable == true)
        {
            SoundManager.Instance.PlaySFX("Plate");
        }
        _isEnable = false;
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



