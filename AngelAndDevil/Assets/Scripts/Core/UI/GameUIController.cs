using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIController : BaseUIController
{
    [Header("Canvas")]
    public Canvas menuCanvas;
    public Canvas soundCanvas;
    public Canvas clearCanvas;
    public Canvas gameOverCanvas;
    [Header("Text")]
    public TMP_Text timeText;
    public TMP_Text angelHostageText;
    public TMP_Text devilHostageText;
    public TMP_Text FinaltimeText;
    [Header("Image")]
    public Image doorStarImage;
    public Image hostageStarImage;
    public Image timeStarImage;
    public Image[] bigStarImages;
    [Header("Num")]
    [SerializeField] private int maxAngelHostage;
    [SerializeField] private int maxDevilHostage;
    private float delayBetweenStars = 1.2f;


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
        doorStarImage.gameObject.SetActive(false);
        hostageStarImage.gameObject.SetActive(false);
        timeStarImage.gameObject.SetActive(false);
        for (int i = 0; i < bigStarImages.Length; i++)
        {
            bigStarImages[i].gameObject.SetActive(false);
        }
        maxAngelHostage = ScoreManager.Instance.AngelHostage;
        maxDevilHostage = ScoreManager.Instance.DevilHostage;
    }

    private void Update()
    {
        if (GameManager.Instance.isClear)//���⿡ �� ȹ�� �߰�, �ڷ�ƾ
        {
            GameManager.Instance.Pause(true);
            FinaltimeText.text = timeText.text;
            clearCanvas.gameObject.SetActive(true);
            StartCoroutine(ShowStars());
        }
        else if (GameManager.Instance.isGameOver)
        {
            GameManager.Instance.Pause(true);
            gameOverCanvas.gameObject.SetActive(true);
        }
        else
        {
            UpdateTimeText();
            UpdateHostageNumber();
        }
    }

    IEnumerator ShowStars()
    {
        bool inTime = GameManager.Instance.GetStageIntime(GameManager.Instance._Stage);//�Ƹ� Set�� �����༭ ���� �߻�
        int score = 0;
        yield return new WaitForSecondsRealtime(delayBetweenStars);
        doorStarImage.gameObject.SetActive(true);
        score++;
        if (GameManager.Instance.GetStageLeftAngel(GameManager.Instance._Stage) == 0
            && GameManager.Instance.GetStageLeftDevil(GameManager.Instance._Stage) == 0)
        {
            yield return new WaitForSecondsRealtime(delayBetweenStars);
            hostageStarImage.gameObject.SetActive(true);
            score++;
        }
        if (inTime)
        {
            yield return new WaitForSecondsRealtime(delayBetweenStars);
            timeStarImage.gameObject.SetActive(true);
            score++;
        }
        for (int i = 0; i < score; i++)
        {
            yield return new WaitForSecondsRealtime(delayBetweenStars);
            bigStarImages[i].gameObject.SetActive(true);
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
                OpenSetting(false);
            }
        }
        else
        {
            OpenSetting(true);
        }
    }

    public void OpenSetting(bool isStop)
    {
        if(isStop)
        {
            OpenUI(menuCanvas.gameObject);
            GameManager.Instance.Pause(true);
        }
        else
        {
            CloseUI(menuCanvas.gameObject);
            GameManager.Instance.Pause(false);
        }
    }

    public void Retry()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        GameManager.Instance.StartGame(GameManager.Instance._Stage);
        SceneManager.LoadScene(currentScene);
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(GameManager.Instance._Time / 60f);
        int seconds = Mathf.FloorToInt(GameManager.Instance._Time % 60f);

        timeText.text = $"{minutes:D2} : {seconds:D2}";
    }

    private void UpdateHostageNumber()
    {
        angelHostageText.text = $"{maxAngelHostage - ScoreManager.Instance.AngelHostage}/{maxAngelHostage}";
        devilHostageText.text = $"{maxDevilHostage - ScoreManager.Instance.DevilHostage}/{maxDevilHostage}";
    }
}
