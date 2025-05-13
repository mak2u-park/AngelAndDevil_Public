using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : BaseUIController
{
    public Canvas stageCanvas;
    public Canvas soundCanvas;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        stageCanvas.gameObject.SetActive(!isStart);
        soundCanvas.gameObject.SetActive(false);
    }
    protected override UIState GetUIState()
    {
        return UIState.Main;
    }

    protected override void OnEsc()
    {
        base.OnEsc();
        if (soundCanvas.gameObject.activeSelf)
        {
            CloseUI(soundCanvas.gameObject);
        }
        else if (stageCanvas.gameObject.activeSelf)
        {
            CloseUI(stageCanvas.gameObject);
        }
        else
        {
            return;
        }
    }
}
