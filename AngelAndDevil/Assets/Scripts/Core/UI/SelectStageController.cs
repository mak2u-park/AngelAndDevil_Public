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

    [SerializeField]private Button clickbutton;

    [SerializeField] private float percent = 0f;//test��
    [SerializeField] private int currentStarNum = 3;//test��
    [SerializeField] private int maxStarNum = 15;//test��

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

        percent = (float)currentStarNum / maxStarNum * 100f;
        Percent.text = $"{percent:N0}%";
        StarNum.text = $"( {currentStarNum} / {maxStarNum} )";
    }

    private void Update()//���⵵ GameManager
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
                //���� ���̿� �ð� 1������ ������ �ֱ�
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
        //���� �� GameManager�� �Ѱܾ���
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
            Debug.Log("���� ��ġ");
        }
    }
}