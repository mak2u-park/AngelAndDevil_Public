using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public abstract class BaseUIController : MonoBehaviour
{
    protected UIManager uiManager;

    public static bool isStart = true;
    //protected static int roomNum = 0; //GameManager에서 관리해주는 것이 좋을듯?

    protected string[] sceneName ={
        "Stage1","Stage2","Stage3","Stage4",
        "Stage1-1","Stage1-2","Stage1-3",
        "Stage2-1", "Stage2-2", "Stage2-3",
        "Stage3-1", "Stage3-2", "Stage3-3",
        "Stage4-1", "Stage4-1", "Stage4-1"
    };


    protected virtual void Awake()
    {
        uiManager = UIManager.Instance;
    }
    protected virtual void Start()
    {
        GameManager.Instance.Pause(false);
    }

    protected abstract UIState GetUIState();

    protected virtual void OnEsc()
    {
        
    }
    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void ChangeScene(int stage)
    {
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.Pause(false);
        if (stage < 4)
        {
            SoundManager.Instance.StopBGM();
            GameManager.Instance.tema = stage;
        }
        SceneManager.LoadScene(sceneName[stage]);
    }

    public void GoMainScene(bool isRestart)
    {
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.Pause(false);
        isStart = isRestart;
        SceneManager.LoadScene("StartSceneUI_juna");
        SoundManager.Instance.StopBGM();
    }
}
