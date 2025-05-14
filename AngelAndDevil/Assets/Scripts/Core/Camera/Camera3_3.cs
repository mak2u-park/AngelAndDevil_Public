using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3_3 : MonoBehaviour
{
    [SerializeField] private GameObject Angel;
    [SerializeField] private GameObject Devil;

    private Vector3 StartCameraPos;

    private void Start()
    {
        if (Angel == null)
        {
            Debug.LogError("Angel 할당 오류");
            return;
        }
        if (Devil == null)
        {
            Debug.LogError("Devil 할당 오류");
            return;
        }
        StartCameraPos = transform.position;
    }

    void FixedUpdate()
    {

        float PosY = (Angel.transform.position.y + Devil.transform.position.y) * 0.5f;
        if (PosY < -0.45f)
        {
            PosY = -0.45f;
        }
        Vector3 Position = new Vector3(StartCameraPos.x, PosY, StartCameraPos.z);

        transform.position = Vector3.Lerp(transform.position, Position, Time.deltaTime * 2);
    }
}
