using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // 유니티 에디터에서 DoorType을 설정할 수 있도록 SerializeField 속성을 사용
    [SerializeField] private DoorType _type;
    // 유니티 에디터에서 DoorPair를 설정할 수 있도록 SerializeField 속성을 사용
    [SerializeField] private Door _doorPair;
    
    // Door와 플레이어 타입이 일치하는지를 나타내는 변수
    private bool DoorMatched = false;

    Animator Animator;

    private void Start()
    {
        if (_doorPair == null)
        {
            Debug.LogError("Door를 연결해 주세요");
        }
        if (_type == _doorPair._type)
        {
            Debug.LogError("Door의 DoorType은 서로 달라야 합니다.");
        }

        Animator = GetComponentInChildren<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_type == DoorType.Angel && collision.gameObject.name == "Angel") // 이 오브젝트가 Angel Door일때
        {
            DoorMatched = true;
            Debug.Log("Angel Door Matched");
            CheckDoorMatched();
        }
        else if (_type == DoorType.Devil && collision.gameObject.name == "Devil") // 이 오브젝트가 Devil Door일때
        {
            DoorMatched = true;
            Debug.Log("Devil Door Matched");
            CheckDoorMatched();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_type == DoorType.Angel && collision.gameObject.name == "Angel") // 이 오브젝트가 Angel Door일때
        {
            DoorMatched = false;
            CheckDoorMatched();
        }
        else if (_type == DoorType.Devil && collision.gameObject.name == "Devil") // 이 오브젝트가 Devil Door일때
        {
            DoorMatched = false;
            CheckDoorMatched();
        }
        
    }

    private void CheckDoorMatched()
    {
        // 두 문에 알맞은 플레이어가 들어갔을때 문이 열리도록 설정
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
