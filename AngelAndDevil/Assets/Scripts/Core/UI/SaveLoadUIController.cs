using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUIController : BaseUIController
{
    [SerializeField] private GameObject saveSlotPrefab;
    [SerializeField] private Transform parenttransform;
    [SerializeField] private GameObject saveslotcanvas;
    private int slotnum;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        if(PlayerPrefs.HasKey(GameManager.Instance.Slotnumberkey))
        {
            slotnum = PlayerPrefs.GetInt(GameManager.Instance.Slotnumberkey);
        }
        else
        {
            slotnum = 0;
        }
        for(int i = 0; i < slotnum; i++)
        {
            GameObject Slot = Instantiate(saveSlotPrefab, parenttransform);
            Slot.GetComponentInChildren<Text>().text = $"File {i + 1} Load";
            SaveSlot saveslot = Slot.GetComponent<SaveSlot>();
            saveslot.SetSlot(i);
        }
        Debug.Log($"{slotnum}�� ����Ǿ�����");
    }
    protected override UIState GetUIState()
    {
        return UIState.SaveLoad;
    }

    public void Onclose()
    {
        CloseUI(saveslotcanvas);
    }

    public void Onclick()
    {
        OpenUI(saveslotcanvas);
    }

    public void SaveNew()
    {
        Debug.Log("����������");
        GameObject Slot = Instantiate(saveSlotPrefab, parenttransform);
        Slot.GetComponentInChildren<Text>().text = $"File {slotnum + 1} Load";
        SaveSlot saveslot = Slot.GetComponent<SaveSlot>();
        saveslot.SetSlot(slotnum);
        slotnum += 1;
        GameManager.Instance.SaveDataOnNewSlot();
        Debug.Log("���� ����!");
    }
}
