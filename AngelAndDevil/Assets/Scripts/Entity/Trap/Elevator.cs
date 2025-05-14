using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Elevator : MonoBehaviour
{
    [SerializeField] Plate plateTop;
    [SerializeField] GameObject elevatorPosition; // 엘리베이터 바닥 오브젝트
    BoxCollider2D colider;
    // 엘리베이터가 올라갈 높이
    [Range(-30, 30), SerializeField] float width = 0f;
    [Range(-30, 30), SerializeField] float height = 2f;

    private float StartX; // 엘리베이터의 시작 X 좌표
    private float EndX; // 엘리베이터의 끝 X 좌표
    private float StartY; // 엘리베이터의 시작 높이
    private float EndY; // 엘리베이터의 끝 높이
    private const float ElevatorSpeed = 2f;
    
    private void Start()
    {   colider = GetComponent<BoxCollider2D>();
        StartX = transform.position.x;
        EndX = StartX + width;
        StartY = transform.position.y;
        EndY = StartY + height;

    }

    private void FixedUpdate()
    {
        if (plateTop == null)
        {
            Debug.LogError("PlateTop이 설정되지 않았습니다.");
            return;
        }
        if (plateTop.IsEnable)
        {
            UpPlate();
        }
        else
        {
            DownPlate();
        }
    }

    private void UpPlate()
    {
        // 현재위치에서 EndY 좌표로 이동
        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, EndX, Time.fixedDeltaTime * ElevatorSpeed);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, EndY, Time.fixedDeltaTime * ElevatorSpeed);
        transform.position = currentPosition;
    }

    private void DownPlate()
    {
        // 현재 위치에서 StartY 좌표로 이동
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, StartX, Time.fixedDeltaTime * ElevatorSpeed);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, StartY, Time.fixedDeltaTime * ElevatorSpeed);
        transform.position = currentPosition;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("엘베에 닿음");
            collision.transform.SetParent(transform); // 발판을 부모로 설정
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null); // 부모 해제
        }
    }

    


}