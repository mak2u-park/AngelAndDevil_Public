using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3_3 : MonoBehaviour
{
    [SerializeField] private GameObject _angel;
    [SerializeField] private GameObject _devil;

    private Vector3 StartCameraPos;

    private const float MinPosY = -0.45f;
    private const float CameraLerpSpeed = 2f;

    private void Start()
    {
        if (_angel == null)
        {
            Debug.LogError("Angel 할당 오류");
            return;
        }
        if (_devil == null)
        {
            Debug.LogError("Devil 할당 오류");
            return;
        }
        StartCameraPos = transform.position;
    }

    void FixedUpdate()
    {

        float PosY = (_angel.transform.position.y + _devil.transform.position.y) * 0.5f; // 중간 위치
        if (PosY < MinPosY)
        {
            PosY = MinPosY;
        }
        Vector3 Position = new Vector3(StartCameraPos.x, PosY, StartCameraPos.z);

        transform.position = Vector3.Lerp(transform.position, Position, Time.deltaTime * CameraLerpSpeed);
    }
}
