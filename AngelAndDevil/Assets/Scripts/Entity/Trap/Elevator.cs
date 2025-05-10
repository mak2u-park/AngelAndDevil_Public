using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] Button button;
    // ���������Ͱ� �ö� ����
    [Range(0, 20), SerializeField] float height = 2f;

    private float StartY; // ������������ ���� ����
    private float EndY; // ������������ �� ����

    private void Start()
    {
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
        currentPosition.y = Mathf.Lerp(currentPosition.y, EndY, Time.fixedDeltaTime);
        transform.position = currentPosition;
    }

    private void DownPlate()
    {
        // ���� ��ġ���� StartY ��ǥ�� �̵�
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Lerp(currentPosition.y, StartY, Time.fixedDeltaTime);
        transform.position = currentPosition;
    }
}