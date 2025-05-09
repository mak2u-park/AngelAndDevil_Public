using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum UIState
{
    Main,
    SellectStage,
    Game,
    Credit
}
public class UIManager : MonoBehaviour
{
    private static bool isStart = true;

    [Header("Notice")]
    public Canvas stageCanvas;
    public Canvas soundCanvas;

    private void Start()
    {
        if (isStart)
        {
            stageCanvas.gameObject.SetActive(false);
        }
        else
        {
            stageCanvas.gameObject.SetActive(true);
        }
        soundCanvas.gameObject.SetActive(false);
    }
    void OnEsc()
    {
        if (SceneManager.GetActiveScene().name == "StartSceneUI_juna")//Scene매니저에서 씬 타입 나누기
        {
            if (soundCanvas.gameObject.activeSelf)
            {
                CloseSound();
            }
            if (stageCanvas.gameObject.activeSelf)
            {
                CloseStage();
            }
            else
            {
                return;
            }
        }
        else
        {
            for (int i = 1; i <= 4; i++)
            {
                if (SceneManager.GetActiveScene().name == "SelectStage"+i)//이것도 나중에 Scene매니저에서 씬 타입 나눠서 해주면 될듯
                {
                    BacktoMain();
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Room1-1")
        {
            if (stageCanvas.gameObject.activeSelf)
            {
                if(soundCanvas.gameObject.activeSelf)
                {
                    CloseSound();
                }
                else
                {
                    CloseStage();
                }
            }
            else
            {
                OpenStage();
            }
        }
    }
    public void OpenSound()
    {
        soundCanvas.gameObject.SetActive(true);
    }

    public void CloseSound()
    {
        soundCanvas.gameObject.SetActive(false);
        return;
    }

    public void OpenStage()
    {
        stageCanvas.gameObject.SetActive(true);
    }

    public void CloseStage()
    {
        stageCanvas.gameObject.SetActive(false);
    }

    public void SelectStage(Button button)
    {
        isStart = false;
        SceneManager.LoadScene("Select" + button.name);//여기도 나중에 Scene매니저에 따라 수정
    }
    public void BacktoMain()
    {
        //if문 써서 Scenetype체크 후, 게임 진행 중이면 isStart = true;
        SceneManager.LoadScene("StartSceneUI_juna");
    }

    public void SelectRoom(Button button)
    {
        isStart = true;
        SceneManager.LoadScene("Room" + button.name);//여기도 나중에 Scene매니저에 따라 수정
    }
    public void SelectStageInRoom()
    {
        isStart = false;
        SceneManager.LoadScene("SelectStage1");
    }
}
