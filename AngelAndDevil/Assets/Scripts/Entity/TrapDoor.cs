using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] Lever lever; 

    Animator[] animators;

    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();
    }
    void Update()
    {
        if (lever == null)
        {
            Debug.LogError("Lever 할당 오류");
            return;
        }

        if (lever.IsEnable)
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
