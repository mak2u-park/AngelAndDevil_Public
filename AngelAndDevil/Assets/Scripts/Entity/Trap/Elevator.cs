using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Plate plateTop;
    [SerializeField] GameObject elevatorPosition; // ���������� �ٴ� ������Ʈ
    BoxCollider2D colider;
    // ���������Ͱ� �ö� ����
    [Range(-30, 30), SerializeField] float width = 0f;
    [Range(-30, 30), SerializeField] float height = 2f;

    private float StartX; // ������������ ���� X ��ǥ
    private float EndX; // ������������ �� X ��ǥ
    private float StartY; // ������������ ���� ����
    private float EndY; // ������������ �� ����

    // ******************************************************************
    // Gizmos�� ����Ͽ� �ð������� ǥ���ϱ� ���� ����(���� ���� ����)
    private Vector3 GizmoPosition; // Gizmo�� ��ġ
    private Quaternion GizmoRotation; // Gizmo�� ȸ��

    private const float ElevatorSpeed = 2f;

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
            GizmoRotation = elevatorPosition.transform.rotation;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(StartX, StartY, 0), new Vector3(EndX, EndY, 0));
            Vector3 center = GizmoPosition;
            Vector3 size = new Vector3(elevatorFloor.size.x, elevatorFloor.size.y, 0);

            Matrix4x4 _gizmoMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, GizmoRotation, Vector3.one);

            Gizmos.DrawWireCube(Vector2.zero, size);

            Gizmos.matrix = _gizmoMatrix; // ������ ��ķ� ����( Gizmos.matrix�� static�̹Ƿ�)
        }

    }
    private void OnValidate()
    {
        SceneView.RepaintAll();
    }

    // ******************************************************************
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
            Debug.LogError("PlateTop�� �������� �ʾҽ��ϴ�.");
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
        // ������ġ���� EndY ��ǥ�� �̵�
        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, EndX, Time.fixedDeltaTime * ElevatorSpeed);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, EndY, Time.fixedDeltaTime * ElevatorSpeed);
        transform.position = currentPosition;
    }

    private void DownPlate()
    {
        // ���� ��ġ���� StartY ��ǥ�� �̵�
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, StartX, Time.fixedDeltaTime * ElevatorSpeed);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, StartY, Time.fixedDeltaTime * ElevatorSpeed);
        transform.position = currentPosition;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("������ ����");
            collision.transform.SetParent(transform); // ������ �θ�� ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null); // �θ� ����
        }
    }

    


}