using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed { get; protected set; } = 3f;
    public float JumpForce { get; protected set; } = 5f;
    public bool IsGrounded { get; protected set; } = false;
    public bool IsDie { get; protected set; } = false;
    
    public bool IsLaund { get; protected set; } = false;

    [SerializeField]protected Vector2 movementDirection = Vector2.zero;
    int groundLayerMask;
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] BoxCollider2D colider;



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
        if(rb.velocity.y > 0) return;
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        SoundManager.Instance.PlaySFX("JumpSound");
        
    }
    
    public virtual void GroundCheck()
    { 
        Vector2 left = new Vector2(transform.position.x - colider.size.x/2, transform.position.y);
        Vector2 right = new Vector2(transform.position.x + colider.size.x / 2, transform.position.y);
        RaycastHit2D Lefthit = Physics2D.Raycast(left, Vector2.down, 0.8f,groundLayerMask);
        RaycastHit2D Righthit = Physics2D.Raycast(right, Vector2.down, 0.8f,groundLayerMask);
        if ((Lefthit.collider!=null|| Righthit.collider!=null) && rb.velocity.y > -0.1f)
        {
            IsGrounded = true;
            animator.SetBool(IsJump, false);
        }
        else
        {
            IsGrounded = false;
            animator.SetBool(IsJump, true);
        }
    }

   


    [ContextMenu("DieTest")]
    public virtual void Die()
    {

        IsDie = true;
        animator.SetTrigger(IsDead);
        GameManager.Instance.invokeGameOver();
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
