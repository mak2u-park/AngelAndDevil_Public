using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Detecting : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Sauron sauron;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    public void SetSauron(Sauron sauron)
    {
        this.sauron = sauron;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<AngelController>() != null)
            {
                sauron.DetectAngel();
                sauron.SetAngel(collision.gameObject);
            }
        }
    }




   

}
