using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 12f;
    
    [Header("Jump Mechanics")]
    [SerializeField] private float jumpPower = 20f;

    [SerializeField] private int maxJumps = 2;
    private int jumpCounter;

    [SerializeField] private float coyoteTime = 0.05f;
    private float coyoteCounter;
    
    [SerializeField] private float gravity = 10f;
    
    [Header("Layers")]
    [SerializeField]  private LayerMask groundLayer;
    
    //unity components
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D  boxCollider;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.gravityScale = gravity;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {   
        //keep track of ground detection
        IsGrounded();
        
        //keep track of coyote time and double jump when used
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = maxJumps - 1;
            //'- 1' is there or else is 1 jump + x amount of maxJumps
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    //move logic
    private void Move()
    {
        //player movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        
        //flip player sprite when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(6, 6, 6);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-6, 6, 6);
    }
    
    //jump logic
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coyoteCounter <= 0 && jumpCounter <= 0)
                return;
            if (IsGrounded())
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                }
                else
                {
                    if (jumpCounter > 0)
                    {
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower); 
                        jumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;
        }
        //adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
    }
    
    //checks whether the player is on ground or not
    //prevent infinite jumping
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}