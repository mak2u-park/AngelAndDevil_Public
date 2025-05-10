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

    private float time = 0f;//건우님하고 조율하기

    private int maxAngelHostage = 3;//test용 ->그냥 여기서 선언해주고 serializeField로 inspector에서 조정해도 좋을 듯
    private int maxDevilHostage = 3;//test용
    [SerializeField]private int angelHostage = 1;//test용
    [SerializeField]private int devilHostage = 2;//test용

    [SerializeField]private bool clear = false;//test용
    [SerializeField]private bool isGameOver = false;//test용
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
