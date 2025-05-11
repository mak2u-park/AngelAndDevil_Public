using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingFloorTrap : MonoBehaviour
{
    [SerializeField] private float _disappearTime = 1f;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(_disappearTime);
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }
}
