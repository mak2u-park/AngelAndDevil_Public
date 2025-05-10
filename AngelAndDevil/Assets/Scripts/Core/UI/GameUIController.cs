using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : BaseUIController
{
    public Canvas menuCanvas;
    public Canvas soundCanvas;
    public TMP_Text timeText;
    public TMP_Text angelHostageText;
    public TMP_Text devilHostageText;

    private float time = 0f;//�ǿ���ϰ� �����ϱ�

    private int maxAngelHostage = 3;//test�� ->�׳� ���⼭ �������ְ� serializeField�� inspector���� �����ص� ���� ��
    private int maxDevilHostage = 3;//test��
    [SerializeField]private int angelHostage = 1;//test��
    [SerializeField]private int devilHostage = 2;//test��
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        menuCanvas.gameObject.SetActive(false);
        soundCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateTimeText();
        UpdateHostageNumber();
    }
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
    protected override void OnEsc()
    {
        if (menuCanvas.gameObject.activeSelf)
        {
            if (soundCanvas.gameObject.activeSelf)
            {
                CloseUI(soundCanvas.gameObject);
            }
            else
            {
                ManageTime(false);
            }
        }
        else
        {
            ManageTime(true);
        }
    }
    public void ManageTime(bool isStop)
    {
        if(isStop)
        {
            OpenUI(menuCanvas.gameObject);
            Time.timeScale = 0f;
        }
        else
        {
            CloseUI(menuCanvas.gameObject);
            Time.timeScale = 1f;
        }
    }

    private void UpdateTimeText()
    {
        time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timeText.text = $"{minutes:D2} : {seconds:D2}";
    }

    private void UpdateHostageNumber()
    {
        angelHostageText.text = $"{maxAngelHostage - angelHostage}/{maxAngelHostage}";
        devilHostageText.text = $"{maxDevilHostage - devilHostage}/{maxDevilHostage}";
    }
}
