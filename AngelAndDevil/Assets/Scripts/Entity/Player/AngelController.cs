using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngelController : PlayerController
{
    
    public void OnMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();
        direction.Normalize();  
        movementDirection = direction;
    }

    public void Gliding()
    {
        if(rb.velocity.y < -0.1f)
        {
            rb.gravityScale = 0.6f;
            
        }
        else
        {
            rb.gravityScale = 1f;
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
