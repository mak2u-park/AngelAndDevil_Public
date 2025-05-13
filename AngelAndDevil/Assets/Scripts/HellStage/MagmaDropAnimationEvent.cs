using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaDropAnimationEvent : MonoBehaviour
{
    BoxCollider2D BoxCollider2D;
    Rigidbody2D rb;
    bool ismagmaDrop = false;
    Animator animator;

    private void Awake()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        BoxCollider2D.enabled = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void MagmaDropStart()
    {
        ismagmaDrop = true;
        animator.speed = 0f;
        BoxCollider2D.enabled = true;
        Invoke("ActivateBoxColider", 2f);
    }

    public void ActivateBoxColider()
    {
        Debug.Log("Activate");
        BoxCollider2D.enabled = true;
    }

    public void MagmaDrop()
    {
        rb.gravityScale = 1f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        animator.speed = 1f;
        ismagmaDrop = false;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

    }

    public void MagmaDestroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (ismagmaDrop)
        {
            MagmaDrop();
        }
    }

}
