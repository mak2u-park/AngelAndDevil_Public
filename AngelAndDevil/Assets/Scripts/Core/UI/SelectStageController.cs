using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SelectStageController : BaseUIController
{
    public Button[] roomButtons;
    public GameObject AgDv;
    [SerializeField]private Button clickbutton;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AgDv.transform.position = new Vector3(-6, -1, 0);
        for (int i = 0; i < roomButtons.Length; i++)
        {
            int index = i;
            roomButtons[i].onClick.AddListener(() => ClickRoom(index + 1, roomButtons[index]));
        }
    }

    private void Update()//여기도 GameManager
    {
        if (clickbutton == null)
        {
            return;
        }
        else
        {
            if (AgDv.transform.position == new Vector3(clickbutton.transform.position.x - 0.1f, -1, 0))
            {
                AgDv.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                foreach (Animator anim in AgDv.GetComponentsInChildren<Animator>())
                {
                    anim.SetBool("IsClick", false);
                }
                //여기 사이에 시간 1초정도 딜레이 넣기
                ChangeScene(clickbutton);
            }
        }
    }


    protected override UIState GetUIState()
    {
        return UIState.SellectStage;
    }
    protected override void OnEsc()
    {
        GoMainScene(false);
    }



    public void ClickRoom(int num,Button button)
    {
        //여기 다 GameManager로 넘겨야함
        roomNum = num;
        clickbutton = button;
        foreach (Animator anim in AgDv.GetComponentsInChildren<Animator>())
        {
            anim.SetBool("IsClick", true);
        }
        if (AgDv.transform.position.x < button.transform.position.x)
        {
            foreach (SpriteRenderer sr in AgDv.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.flipX = false;
            }
            AgDv.GetComponent<Rigidbody2D>().velocity = Vector2.right;
        }
        else if (AgDv.transform.position.x > button.transform.position.x)
        {
            foreach (SpriteRenderer sr in AgDv.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.flipX = true;
            }
            AgDv.GetComponent<Rigidbody2D>().velocity = Vector2.left;
        }
        else
        {
            Debug.Log("같은 위치");
        }
    }
}