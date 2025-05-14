using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Plate plateTop;
    [SerializeField] GameObject elevatorPosition; // 엘리베이터 바닥 오브젝트

    // 엘리베이터가 올라갈 높이
    [Range(-30, 30), SerializeField] float width = 0f;
    [Range(-30, 30), SerializeField] float height = 2f;

    private float StartX; // 엘리베이터의 시작 X 좌표
    private float EndX; // 엘리베이터의 끝 X 좌표
    private float StartY; // 엘리베이터의 시작 높이
    private float EndY; // 엘리베이터의 끝 높이

    // ******************************************************************
    // Gizmos를 사용하여 시각적으로 표시하기 위한 변수(차후 삭제 예정)
    private Vector3 GizmoPosition; // Gizmo의 위치

    [ExecuteAlways]

    private void OnDrawGizmos()
    {
        var elevatorFloor = GetComponent<BoxCollider2D>();
        

        if (elevatorFloor != null)
        {

            StartX = elevatorPosition.transform.position.x;
            StartY = elevatorPosition.transform.position.y;
            EndX = StartX + width;
            EndY = StartY + height;
            GizmoPosition = new Vector3(StartX + width, StartY + height, 0);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(StartX, StartY, 0), new Vector3(EndX, EndY, 0));
            Vector3 center = GizmoPosition;
            Vector3 size = new Vector3(elevatorFloor.size.x, elevatorFloor.size.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
        
    }
    private void OnValidate()
    {
        SceneView.RepaintAll();
    }

    // ******************************************************************
    private void Start()
    {
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
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, EndX, Time.fixedDeltaTime * 2f);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, EndY, Time.fixedDeltaTime * 2f);
        transform.position = currentPosition;
    }

    private void DownPlate()
    {
        // 현재 위치에서 StartY 좌표로 이동
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, StartX, Time.fixedDeltaTime * 2f);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, StartY, Time.fixedDeltaTime * 2f);
        transform.position = currentPosition;
    }

    

    
}