using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public abstract class BaseUIController : MonoBehaviour
{
    protected UIManager uiManager;

    public static bool isStart = true;
    protected static int roomNum = 0; //GameManager에서 관리해주는 것이 좋을듯?

    protected virtual void Awake()
    {
        uiManager = UIManager.Instance;
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

    public void ChangeScene(Button button)
    {
        SceneManager.LoadScene(button.name);
    }

    public void GoMainScene(bool isRestart)
    {
        roomNum = 0;
        isStart = isRestart;
        SceneManager.LoadScene("StartSceneUI_juna");
    }
}
