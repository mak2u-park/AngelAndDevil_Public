using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    private int slotIndex;
    [SerializeField] private Button SlotButton;

    public void SetSlot(int index)
    {
        slotIndex = index;
        SlotButton.onClick.AddListener(ChoiceSlot);
    }

    private void ChoiceSlot()
    {
        GameManager.Instance.LoadData(slotIndex);
        GameObject canvas = transform.root.gameObject;
        canvas.gameObject.SetActive(false);

    }
}
