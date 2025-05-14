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