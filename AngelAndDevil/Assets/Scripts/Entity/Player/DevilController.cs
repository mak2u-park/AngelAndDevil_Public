using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DevilController : PlayerController
{
    int ventLayerMask;
    Vector2 moveToVentMid;
    Vector2 desirePos;
    static readonly int Vent = Animator.StringToHash("IsVent");
    bool isVenting = false;
    float VentingDelayTime = 1.08f;
    float VentingTime = 0f;
    
    public void OnMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();
        movementDirection = direction;
        if ( direction.y < 0 && !isVenting)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, ventLayerMask);
            if (hit.collider != null)
            {
                isVenting = true;
                Debug.Log("VentingTrue");
                moveToVentMid = hit.collider.GetComponent<Vent>().GetVentTransform();
                desirePos = hit.collider.GetComponent<Vent>().GetOppositVentTransform();
                VentAnimation();
                
            }
        }
    }

 

    public void VentAnimation()
    {
        animator.SetTrigger(Vent);
    }

    public void VentTranslate()
    {
        transform.position = desirePos;
    }

    public void VentingComplete()
    {
        isVenting = false;
        VentingTime = 0f;
    }


    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
        ventLayerMask = 1 << LayerMask.NameToLayer("Vent");
    }

    protected override void FixedUpdate()
    {
        if (isVenting)
        {
            if (VentingDelayTime > VentingTime)
            {
                transform.position = Vector2.MoveTowards(transform.position, moveToVentMid, Speed * Time.deltaTime);
                VentingTime += Time.deltaTime;
            }
            else
            {
                return;
            }
        }
        base.FixedUpdate();
    }

}
