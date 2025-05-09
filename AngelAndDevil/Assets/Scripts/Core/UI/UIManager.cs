using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    }
    void OnEsc()
    {
        if (soundCanvas.gameObject.activeSelf)
        {
            CloseSound();
        }
        if (SceneManager.GetActiveScene().name == "StartSceneUI_juna")//Scene�Ŵ������� �� Ÿ�� ������
        {
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
                if (SceneManager.GetActiveScene().name == "SelectStage"+i)//�̰͵� ���߿� Scene�Ŵ������� �� Ÿ�� ������ ���ָ� �ɵ�
                {
                    BacktoMain();
                }
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
        SceneManager.LoadScene("Select" + button.name);//���⵵ ���߿� Scene�Ŵ����� ���� ����
    }
    public void BacktoMain()
    {
        //if�� �Ἥ Scenetypeüũ ��, ���� ���� ���̸� isStart = true;
        SceneManager.LoadScene("StartSceneUI_juna");
    }

    public void SelectRoom(Button button)
    {
        SceneManager.LoadScene("Room" + button.name);//���⵵ ���߿� Scene�Ŵ����� ���� ����
    }
}
