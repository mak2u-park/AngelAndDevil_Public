using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserDetector : MonoBehaviour, IEnable
{
    [SerializeField] private LayerMask _laserLayerMask;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float intervalTime = 1f; // 스프라이트 변경 간격(초)

    [Header("Camera Settings")]
    [SerializeField] private Color Startcolor = Color.white; 
    [SerializeField] private Color Endcolor = Color.blue;
    [SerializeField] private float zoomOutSize = 10f; // 카메라 줌 아웃 크기 설정
    [SerializeField] private float zoomInSize = 5f; // 카메라 줌 인 크기 설정

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Coroutine changeCoroutine;
    private bool isTriggered = false;
    private bool _isEnable;
    private int _gimmicCount = 0;  // 기믹 1회 제한
    private Vector2 currentCameraPos;

    public bool IsCameraStop = false;

    [SerializeField] Camera mainCamera;

    // 다른 스크립트에서 사용할 수 있도록 public으로 설정
    public bool IsEnable => _isEnable;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0]; // 첫 번째 스프라이트로 초기화
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
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

            // 스프라이트 변경
            spriteRenderer.sprite = sprites[currentIndex];

            // 배경 색 변경
            float t = (float)currentIndex / (sprites.Length - 1);
            mainCamera.backgroundColor = Color.Lerp(Startcolor, Endcolor, t);

            if (_gimmicCount != 0)
            {
                break;
            }

            IsCameraStop = true;

            // 카메라의 현재 위치 저장
            currentCameraPos = mainCamera.transform.position;

            mainCamera.transform.position = new Vector3(-3.71f, 13.38f, -10f);
            mainCamera.orthographicSize = Mathf.Lerp(zoomInSize, zoomOutSize, t);

            if (currentIndex == sprites.Length - 1)
            {
                if (_gimmicCount == 0)
                {
                    Invoke("resetCamera", 1f);
                }

                Enable();
                _gimmicCount++;
            }

        }

    }

    private void resetCamera()
    {
        mainCamera.transform.position = currentCameraPos;
        IsCameraStop = false;
    }

    // 충돌 해제 시 순차적으로 이전 스프라이트로 이동
    private IEnumerator ChangeSpriteDown()
    {
        while (!isTriggered && currentIndex > 0)
        {
            yield return new WaitForSeconds(intervalTime);
            currentIndex--;

            // 스프라이트 변경
            spriteRenderer.sprite = sprites[currentIndex];

            // 배경 색 변경
            float t = (float)currentIndex / (sprites.Length - 1);
            mainCamera.backgroundColor = Color.Lerp(Startcolor, Endcolor, t);

            if (currentIndex != sprites.Length - 1)
            {
                Disable();
            }
        }

    }


    public void Enable()
    {
        _isEnable = true;
        // 추가 활성화 로직
    }

    public void Disable()
    {
        _isEnable = false;
        // 추가 비활성화 로직
    }
}
