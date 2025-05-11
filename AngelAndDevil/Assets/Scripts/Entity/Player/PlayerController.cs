using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed { get; protected set; } = 3f;
    public float JumpForce { get; protected set; } = 1f;
    public bool IsGrounded { get; protected set; } = false;
    public bool IsDie { get; protected set; } = false;
    
    public bool IsLaund { get; protected set; } = false;

    protected Vector2 movementDirection = Vector2.zero;
    int groundLayerMask;
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] SpriteRenderer _renderer;
  



    protected virtual void Start()
    {
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }


    public virtual void Movement(Vector2 _direction)
    {
        
        Vector2 movePos = new Vector2(_direction.x * Speed, 0);
        transform.Translate(movePos * Time.deltaTime);
        if(_direction.x > 0)
        {
            _renderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _renderer.flipX = true;
        }
        if (Mathf.Abs(_direction.x)>0.1f)
        {
            animator.SetBool(IsRun, true);
        }
        else
        {
            animator.SetBool(IsRun, false);
        }
        if (_direction.y>0 && IsGrounded)
        {
            //Debug.Log("점프한다");
            Jump();//점프
        }
        

    }

    public virtual void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce,ForceMode2D.Impulse);
        
    }
    
    public virtual void GroundCheck()
    { 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f,groundLayerMask);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f,Color.red, 1f);
        if (hit.collider!=null && rb.velocity.y > -0.1f)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

   


    [ContextMenu("DieTest")]
    public virtual void Die()
    {

        IsDie = true;
        animator.SetTrigger(IsDead);
        
    }

    protected virtual void Update()
    {
        GroundCheck();
    }

    protected virtual void FixedUpdate() 
    {
        if (IsDie ) return;
        Movement(movementDirection);

    }

    
}
