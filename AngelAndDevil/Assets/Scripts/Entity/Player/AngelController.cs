using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngelController : PlayerController
{
    private static readonly int IsGlide = Animator.StringToHash("IsGlide");

    public void OnMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();
        movementDirection = direction;
    }

    public void Gliding()
    {
        if(rb.velocity.y < 0f && movementDirection.y > 0f)
        {
            rb.gravityScale = 0.2f;
            animator.SetBool(IsGlide, true);

        }
        else
        {
            rb.gravityScale = 1f;
            animator.SetBool(IsGlide, false);
        }
    }


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Gliding();

    }

}
