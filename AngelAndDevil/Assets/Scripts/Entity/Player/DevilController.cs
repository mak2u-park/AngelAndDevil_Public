using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevilController : PlayerController
{
    public void OnMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();
        direction.Normalize();
        movementDirection = direction;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
