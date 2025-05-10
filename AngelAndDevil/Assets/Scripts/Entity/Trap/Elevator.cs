using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] Button button;

    Animator animators;

    private void Start()
    {
        animators = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (button == null)
        {
            Debug.LogError("Button �Ҵ� ����");
            return;
        }

        if (button.IsEnable)
        {
            UpPlate();
        }
        else
        {
            DownPlate();
        }
    }

    public void UpPlate()
    {
        animators.SetBool("IsUp", true);
    }

    public void DownPlate()
    {
        animators.SetBool("IsUp", false);
    }
}
