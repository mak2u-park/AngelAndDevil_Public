using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserDetector : MonoBehaviour, IEnable
{
    [SerializeField] private LayerMask _laserLayerMask;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float intervalTime = 1f; // ��������Ʈ ���� ����(��)

    [Header("Camera Settings")]
    [SerializeField] private Color Startcolor = Color.white; 
    [SerializeField] private Color Endcolor = Color.blue;
    [SerializeField] private float zoomOutSize = 10f; // ī�޶� �� �ƿ� ũ�� ����
    [SerializeField] private float zoomInSize = 5f; // ī�޶� �� �� ũ�� ����

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Coroutine changeCoroutine;
    private bool isTriggered = false;
    private bool _isEnable;
    private int _gimmicCount = 0;  // ��� 1ȸ ����
    private Vector2 currentCameraPos;

    public bool IsCameraStop = false;

    [SerializeField] Camera mainCamera;

    // �ٸ� ��ũ��Ʈ���� ����� �� �ֵ��� public���� ����
    public bool IsEnable => _isEnable;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0]; // ù ��° ��������Ʈ�� �ʱ�ȭ
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

    // �浹 �� ���������� ���� ��������Ʈ�� �̵�
    private IEnumerator ChangeSpriteUp()
    {
        while (isTriggered && currentIndex < sprites.Length - 1)
        {
            yield return new WaitForSeconds(intervalTime);
            currentIndex++;

            // ��������Ʈ ����
            spriteRenderer.sprite = sprites[currentIndex];

            // ��� �� ����
            float t = (float)currentIndex / (sprites.Length - 1);
            mainCamera.backgroundColor = Color.Lerp(Startcolor, Endcolor, t);

            if (_gimmicCount != 0)
            {
                break;
            }

            IsCameraStop = true;

            // ī�޶��� ���� ��ġ ����
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

    // �浹 ���� �� ���������� ���� ��������Ʈ�� �̵�
    private IEnumerator ChangeSpriteDown()
    {
        while (!isTriggered && currentIndex > 0)
        {
            yield return new WaitForSeconds(intervalTime);
            currentIndex--;

            // ��������Ʈ ����
            spriteRenderer.sprite = sprites[currentIndex];

            // ��� �� ����
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
        // �߰� Ȱ��ȭ ����
    }

    public void Disable()
    {
        _isEnable = false;
        // �߰� ��Ȱ��ȭ ����
    }
}
