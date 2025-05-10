using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IEnable
{
    Rigidbody2D _rigidbody2D;
    private bool isEnable;
    private bool isContact = false;

    // Gate���� ����� �� �ֵ��� public���� ����
    public bool IsEnable => isEnable;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Lever�� ȸ������ ����
        _rigidbody2D.centerOfMass = new Vector2(0f, -0.25f);

        // ������Ʈ �̸��� Lever_Top���� Ȯ��
        if (gameObject.name != "Lever1_Top" && gameObject.name != "Lever2_Top")
        {
            Debug.LogError("�߸��� ������Ʈ�Դϴ� - Lever_Top ����");
            return;
        }
    }

    // ���������̱⿡ FixedUpdate���� ���
    private void FixedUpdate()
    {
        float rawZ = _rigidbody2D.transform.localEulerAngles.z;
        float angleZ = (rawZ > 180f) ? rawZ - 360f : rawZ;
        float clampedZ;

        if ( gameObject.name == "Lever1_Top")
        {
             clampedZ = Mathf.Clamp(angleZ, -90f, 0f);
            
            // ������ �������� ���������� �߷��� ������ �޴� ��ó�� �
            if (clampedZ < -45f)
            {
                clampedZ = Mathf.Lerp(clampedZ, -90f, Time.fixedDeltaTime * 2f); // �ε巴�� ȸ��
            }
            else if (clampedZ > -45f)
            {
                clampedZ = Mathf.Lerp(clampedZ, 0f, Time.fixedDeltaTime * 2f); // �ε巴�� ȸ��
            }
            else
            {
                // ��Ȯ�� 45���϶��� �ƹ��ϵ� �Ͼ�� ����
            }

            // ���� �߽�(centerOfMass) ��ġ�� �������� ȸ��
            _rigidbody2D.MoveRotation(clampedZ); 
        }
        else
        {
            clampedZ = Mathf.Clamp(angleZ, 0f, 90f);

            // ������ �������� ���������� �߷��� ������ �޴� ��ó�� �
            if (clampedZ < 45f)
            {
                clampedZ = Mathf.Lerp(clampedZ, 0f, Time.fixedDeltaTime * 2f); // �ε巴�� ȸ��
            }
            else if (clampedZ > 45f)
            {
                clampedZ = Mathf.Lerp(clampedZ, 90f, Time.fixedDeltaTime * 2f); // �ε巴�� ȸ��
            }
            else
            {
                // ��Ȯ�� 45���϶��� �ƹ��ϵ� �Ͼ�� ����
            }

            // ���� �߽�(centerOfMass) ��ġ�� �������� ȸ��
            _rigidbody2D.MoveRotation(clampedZ); 
        }
        

        // ������ ȸ�� ������ ���밪���� ��ȯ
        float clampedZAbs = Mathf.Abs(clampedZ);

        if (!isContact) return;

        if (clampedZAbs > 45f)
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
