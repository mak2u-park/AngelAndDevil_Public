using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Lever Lever_Top; 

    Animator[] animators;

    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();

        if (Lever_Top == null)
        {
            Debug.LogError("Lever �Ҵ� ����");
            return;
        }

        if (gameObject.name != "Gate")
        {
            Debug.LogError("�߸��� ������Ʈ�Դϴ� - Gate ����");
            return;
        }
    }

    private void FixedUpdate()
    {
        if (Lever_Top.IsEnable)
        {
            OpenTrapDoor();
        }
        else
        {
            CloseTrapDoor();
        }
    }

    public void OpenTrapDoor()
    {
        animators[0].SetBool("IsOpen", true);
        animators[1].SetBool("IsOpen", true);
    }

    public void CloseTrapDoor()
    {
        animators[0].SetBool("IsOpen", false);
        animators[1].SetBool("IsOpen", false);
    }
}
