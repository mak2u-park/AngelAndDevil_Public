using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] Button button;
    // 엘리베이터가 올라갈 높이
    [Range(0, 20), SerializeField] float height = 2f;

    private float StartY; // 엘리베이터의 시작 높이
    private float EndY; // 엘리베이터의 끝 높이

    private void Start()
    {
        StartY = transform.position.y;
        EndY = StartY + height;

        // 오브젝트 이름이 ElevatorFloor인지 확인
        if (gameObject.name != "ElevatorFloor")
        {
            Debug.LogError("잘못된 오브젝트입니다 - ElevatorFloor 전용");
            return;
        }
    }

    private void FixedUpdate()
    {
        if (button.IsEnable)
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
        currentPosition.y = Mathf.Lerp(currentPosition.y, EndY, Time.fixedDeltaTime);
        transform.position = currentPosition;
    }

    private void DownPlate()
    {
        // 현재 위치에서 StartY 좌표로 이동
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Lerp(currentPosition.y, StartY, Time.fixedDeltaTime);
        transform.position = currentPosition;
    }
}