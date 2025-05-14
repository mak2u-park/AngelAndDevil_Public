using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetector : MonoBehaviour, IEnable
{
    [SerializeField] private LayerMask _laserLayerMask;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float intervalTime = 1f; // 스프라이트 변경 간격(초)

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Coroutine changeCoroutine;
    private bool isTriggered = false;
    private bool isEnable;

    // 다른 스크립트에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => isEnable;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0]; // 첫 번째 스프라이트로 초기화
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

    // 충돌 시 순차적으로 다음 스프라이트로 이동
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

    // 충돌 해제 시 순차적으로 이전 스프라이트로 이동
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
        // 추가 활성화 로직
    }

    public void Disable()
    {
        isEnable = false;
        // 추가 비활성화 로직
    }
}
