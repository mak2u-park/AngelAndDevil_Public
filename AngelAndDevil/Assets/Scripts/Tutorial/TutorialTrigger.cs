using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] GameObject tutorialImage;
    int enterCount = 0;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            enterCount++;
            tutorialImage.SetActive(true);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            enterCount--;
            if (enterCount == 0)
            {
                tutorialImage.SetActive(false);

            }
        }
    }
}