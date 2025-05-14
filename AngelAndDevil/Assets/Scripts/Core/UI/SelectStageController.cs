using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SelectStageController : BaseUIController
{
    public Button[] roomButtons;
    public GameObject AgDv;
    public TMP_Text Percent;
    public TMP_Text StarNum;

    private Button clickbutton;

    private float percent = 0f;
    private int currentStarNum;//test용
    private int maxStarNum = 9;//test용

    private int indexnum;
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        AgDv.transform.position = GameManager.Instance.AgDvPosition[GameManager.Instance.tema];
        currentStarNum = GameManager.Instance.GetStageScore(GameManager.Instance.tema + 1)
                + GameManager.Instance.GetStageScore(GameManager.Instance.tema + 2)
                + GameManager.Instance.GetStageScore(GameManager.Instance.tema + 3);


        for (int i = 0; i < roomButtons.Length; i++)
        {
            int index = i;
            roomButtons[i].onClick.AddListener(() => ClickRoom(roomButtons[index], index));
        }

        percent = (float)currentStarNum / maxStarNum * 100f;
        Percent.text = $"{percent:N0}%";
        StarNum.text = $"( {currentStarNum} / {maxStarNum} )";
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
                GameManager.Instance.AgDvPosition[GameManager.Instance.tema]= new Vector3(clickbutton.transform.position.x - 0.1f, -1, 0);
                Debug.Log(GameManager.Instance.AgDvPosition[GameManager.Instance.tema].x + " " + GameManager.Instance.AgDvPosition[GameManager.Instance.tema].y + " " + GameManager.Instance.AgDvPosition[GameManager.Instance.tema].z);
                GameManager.Instance.StartGame(GameManager.Instance.tema * 3 + indexnum + 1);//3은 한 테마당 스테이 수
                ChangeScene(GameManager.Instance.tema * 3 + indexnum + 4);//4는 테마 수 -> 나중에 확장성 챙길려면 변수로 바꿔야할 듯
                
                
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



    public void ClickRoom(Button button,int index)
    {
        bool tryClick = GameManager.Instance.TrySelectStage(GameManager.Instance.tema * 3 + index + 1);
        if (tryClick)
        {
            Debug.Log("진입");
            indexnum = index;
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
        else
        {
            Debug.Log("실패");
        }
        
    }
}