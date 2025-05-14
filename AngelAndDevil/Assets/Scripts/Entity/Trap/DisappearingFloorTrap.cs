using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingFloorTrap : MonoBehaviour
{
    [SerializeField] private float _disappearTime = 1f;
    private bool isDisappearing = false;

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(_disappearTime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && !isDisappearing)
        {
            if(this.gameObject.activeSelf)
            {
                isDisappearing = true;
                StartCoroutine(Disappear());
            }
        }
    }
}
