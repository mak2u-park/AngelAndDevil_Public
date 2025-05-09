using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool isEnable;
    private bool isContact = false;

    // TrapDoor���� ����� �� �ֵ��� public���� ����
    public bool IsEnable => isEnable;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Lever�� ȸ������ ����
        _rigidbody2D.centerOfMass = new Vector2(0f, -0.25f);
    }

    // ���������̱⿡ FixedUpdate���� ���
    void FixedUpdate()
    {
        float rawZ = _rigidbody2D.transform.localEulerAngles.z;
        float angleZ = (rawZ > 180f) ? rawZ - 360f : rawZ;
        float clampedZ;

        if ( gameObject.name == "Lever1_Top")
        {
             clampedZ = Mathf.Clamp(angleZ, -90f, 0f);
            _rigidbody2D.MoveRotation(clampedZ); // ���� �߽�(centerOfMass) ��ġ�� �������� ȸ��
        }
        else if (gameObject.name == "Lever2_Top")
        {
            clampedZ = Mathf.Clamp(angleZ, 0f, 90f);
            _rigidbody2D.MoveRotation(clampedZ); // ���� �߽�(centerOfMass) ��ġ�� �������� ȸ��
        }
        else
        {
            Debug.LogError("���� �̸� ����");
            return;
        }

        // ������ ȸ�� ������ ���밪���� ��ȯ
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
        // �߰� Ȱ��ȭ ����
        Debug.Log("���� Ȱ��ȭ");
    }

    public void Disable()
    {
        isEnable = false;
        // �߰� ��Ȱ��ȭ ����
        Debug.Log("���� ��Ȱ��ȭ");
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
