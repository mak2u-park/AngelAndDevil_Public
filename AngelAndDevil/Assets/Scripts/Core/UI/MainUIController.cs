using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainUIController : BaseUIController
{
    public Canvas stageCanvas;
    public Canvas soundCanvas;
    protected override UIState GetUIState()
    {
        return UIState.Main;
    }

    protected override void OnEsc()
    {
        base.OnEsc();
        if (soundCanvas.gameObject.activeSelf)
        {
            CloseCanvas(soundCanvas);
        }
        if (stageCanvas.gameObject.activeSelf)
        {
            CloseCanvas(stageCanvas);
        }
        else
        {
            return;
        }
    }

    public void OpenSound()
    {
        soundCanvas.gameObject.SetActive(true);
    }

    public void OpenStage()
    {
        stageCanvas.gameObject.SetActive(true);
    }
    public void CloseCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }

}
