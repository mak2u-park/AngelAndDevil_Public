using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // ����Ƽ �����Ϳ��� DoorType�� ������ �� �ֵ��� SerializeField �Ӽ��� ���
    [SerializeField] private DoorType _type;
    // ����Ƽ �����Ϳ��� DoorPair�� ������ �� �ֵ��� SerializeField �Ӽ��� ���
    [SerializeField] private Door _doorPair;
    
    // Door�� �÷��̾� Ÿ���� ��ġ�ϴ����� ��Ÿ���� ����
    private bool DoorMatched = false;

    Animator Animator;

    private void Start()
    {
        if (_doorPair == null)
        {
            Debug.LogError("Door�� ������ �ּ���");
        }
        if (_type == _doorPair._type)
        {
            Debug.LogError("Door�� DoorType�� ���� �޶�� �մϴ�.");
        }

        Animator = GetComponentInChildren<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_type == DoorType.Angel && collision.gameObject.name == "Angel") // �� ������Ʈ�� Angel Door�϶�
        {
            DoorMatched = true;
            Debug.Log("Angel Door Matched");
            CheckDoorMatched();
        }
        else if (_type == DoorType.Devil && collision.gameObject.name == "Devil") // �� ������Ʈ�� Devil Door�϶�
        {
            DoorMatched = true;
            Debug.Log("Devil Door Matched");
            CheckDoorMatched();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_type == DoorType.Angel && collision.gameObject.name == "Angel") // �� ������Ʈ�� Angel Door�϶�
        {
            DoorMatched = false;
            CheckDoorMatched();
        }
        else if (_type == DoorType.Devil && collision.gameObject.name == "Devil") // �� ������Ʈ�� Devil Door�϶�
        {
            DoorMatched = false;
            CheckDoorMatched();
        }
        
    }

    private void CheckDoorMatched()
    {
        // �� ���� �˸��� �÷��̾ ������ ���� �������� ����
        if (DoorMatched && _doorPair.DoorMatched)
        {
            Debug.Log("Door Opened");
            Animator.SetBool("IsOpen", true);
            _doorPair.Animator.SetBool("IsOpen", true);
            GameManager.Instance.Clear();
        }
        else
        {
            Debug.Log("Door Closed");
            Animator.SetBool("IsOpen", false);
            _doorPair.Animator.SetBool("IsOpen", false);
            GameManager.Instance.Clear();
        }
    }

}
