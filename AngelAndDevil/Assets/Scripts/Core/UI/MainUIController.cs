using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainUIController : BaseUIController
{
    public Canvas stageCanvas;
    public Canvas soundCanvas;

    private static bool[] canWalk;

    public Image[] stage2images;
    public Image[] stage3images;
    public Image[] stage4images;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        for (int i = 0; i < stage2images.Length; i++)
        {
            bool tryClick = GameManager.Instance.TrySelectStage(3);
            if (tryClick)
            {
                Color color = stage2images[i].color;
                color.a = 1.0f;
                stage2images[i].color = color;
            }
            else
            {
                Color color = stage2images[i].color;
                color.a = 0.3f;
                stage2images[i].color = color;
            }
        }
        for (int i = 0; i < stage3images.Length; i++)
        {
            bool tryClick = GameManager.Instance.TrySelectStage(6);
            if (tryClick)
            {
                Color color = stage3images[i].color;
                color.a = 1.0f;
                stage3images[i].color = color;
            }
            else
            {
                Color color = stage3images[i].color;
                color.a = 0.3f;
                stage3images[i].color = color;
            }
        }
        for (int i = 0; i < stage4images.Length; i++)
        {
            bool tryClick = GameManager.Instance.TrySelectStage(9);
            if (tryClick)
            {
                Color color = stage4images[i].color;
                color.a = 1.0f;
                stage4images[i].color = color;
            }
            else
            {
                Color color = stage4images[i].color;
                color.a = 0.3f;
                stage4images[i].color = color;
            }
        }
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

    public void SelectTema(int num)
    {
        bool tryClick;
        if (num == 0)
        {
            tryClick = true;
        }
        else
        {
            tryClick = GameManager.Instance.TrySelectStage(num * 3);
        }
        if (tryClick)
        {
            ChangeScene(num);
        }
        else
        {
            return;
        }
    }
}
