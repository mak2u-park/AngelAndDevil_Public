using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] Button button;
    // ���������Ͱ� �ö� ����
    [Range(0, 20), SerializeField] float width = 0f;
    [Range(0, 20), SerializeField] float height = 2f;

    private float StartX; // ������������ ���� X ��ǥ
    private float EndX; // ������������ �� X ��ǥ
    private float StartY; // ������������ ���� ����
    private float EndY; // ������������ �� ����

    private void Start()
    {
        StartX = transform.position.x;
        EndX = StartX + width;
        StartY = transform.position.y;
        EndY = StartY + height;

        // ������Ʈ �̸��� ElevatorFloor���� Ȯ��
        if (gameObject.name != "ElevatorFloor")
        {
            Debug.LogError("�߸��� ������Ʈ�Դϴ� - ElevatorFloor ����");
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
        // ������ġ���� EndY ��ǥ�� �̵�
        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, EndX, Time.fixedDeltaTime * 2f);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, EndY, Time.fixedDeltaTime * 2f);
        transform.position = currentPosition;
    }

    private void DownPlate()
    {
        // ���� ��ġ���� StartY ��ǥ�� �̵�
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, StartX, Time.fixedDeltaTime * 2f);
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, StartY, Time.fixedDeltaTime * 2f);
        transform.position = currentPosition;
    }
}