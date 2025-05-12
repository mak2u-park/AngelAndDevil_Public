using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIController : BaseUIController
{
    public Canvas menuCanvas;
    public Canvas soundCanvas;
    public Canvas clearCanvas;
    public Canvas gameOverCanvas;
    public TMP_Text timeText;
    public TMP_Text angelHostageText;
    public TMP_Text devilHostageText;
    public TMP_Text FinaltimeText;

    private float time = 0f;//�ǿ���ϰ� �����ϱ�

    private int maxAngelHostage = 3;//test�� ->�׳� ���⼭ �������ְ� serializeField�� inspector���� �����ص� ���� ��
    private int maxDevilHostage = 3;//test��
    [SerializeField]private int angelHostage = 1;//test��
    [SerializeField]private int devilHostage = 2;//test��

    [SerializeField]private bool clear = false;//test��
    [SerializeField]private bool isGameOver = false;//test��
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        menuCanvas.gameObject.SetActive(false);
        soundCanvas.gameObject.SetActive(false);
        clearCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (clear)
        {
            Time.timeScale = 0f;
            FinaltimeText.text = timeText.text;
            clearCanvas.gameObject.SetActive(true);
        }
        else if (isGameOver)
        {
            Time.timeScale = 0f;
            gameOverCanvas.gameObject.SetActive(true);
        }
        else
        {
            UpdateTimeText();
            UpdateHostageNumber();
        }
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

    public void Retry(Button button)
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(button.name);
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
