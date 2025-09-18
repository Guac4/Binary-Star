using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public Animator _animator;
    bool facingRight = true;

    bool grounded;

    public Rigidbody2D rb2d;


    private float _movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocityX = _movement;
    }

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        Vector3 normal = other.GetContact(0).normal;
    //        if(normal == Vector3.up)
    //        {
    //            grounded = true;
    //        }

    //    }
    //}
    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        grounded = false;
    //    }
    //}

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
        if (_movement != 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        if (_movement > 0 && !facingRight)
        {
            Flip();
        }
        if (_movement < 0 && facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1 && isGrounded())
        {
           rb2d.linearVelocityY = jumpHeight;
        }
    }
}
