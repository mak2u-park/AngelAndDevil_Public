using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetector : MonoBehaviour, IEnable
{
    [SerializeField] private LayerMask _laserLayerMask;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float intervalTime = 1f; // ��������Ʈ ���� ����(��)

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Coroutine changeCoroutine;
    private bool isTriggered = false;
    private bool isEnable;

    // �ٸ� ��ũ��Ʈ���� ����� �� �ֵ��� public���� ����
    public bool IsEnable => isEnable;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0]; // ù ��° ��������Ʈ�� �ʱ�ȭ
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _laserLayerMask) != 0)
        {
            isTriggered = true;
            if (changeCoroutine != null)
                StopCoroutine(changeCoroutine);
            changeCoroutine = StartCoroutine(ChangeSpriteUp());

            
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _laserLayerMask) != 0)
        {
            isTriggered = false;
            if (changeCoroutine != null)
                StopCoroutine(changeCoroutine);
            changeCoroutine = StartCoroutine(ChangeSpriteDown());
        }
        
    }

    // �浹 �� ���������� ���� ��������Ʈ�� �̵�
    private IEnumerator ChangeSpriteUp()
    {
        while (isTriggered && currentIndex < sprites.Length - 1)
        {
            yield return new WaitForSeconds(intervalTime);
            currentIndex++;
            spriteRenderer.sprite = sprites[currentIndex];
            if (currentIndex == sprites.Length - 1)
            {
                Enable();
            }
        }
    }

    // �浹 ���� �� ���������� ���� ��������Ʈ�� �̵�
    private IEnumerator ChangeSpriteDown()
    {
        while (!isTriggered && currentIndex > 0)
        {
            yield return new WaitForSeconds(intervalTime);
            currentIndex--;
            spriteRenderer.sprite = sprites[currentIndex];
            if (currentIndex != sprites.Length - 1)
            {
                Disable();
            }
        }

    }
    public void Enable()
    {
        isEnable = true;
        // �߰� Ȱ��ȭ ����
    }

    public void Disable()
    {
        isEnable = false;
        // �߰� ��Ȱ��ȭ ����
    }
}
