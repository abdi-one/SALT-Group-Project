using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
<<<<<<< HEAD
    //code for adjusting game logic or changing variable
=======
>>>>>>> a79e5f80da289a271d05feb990e4633d4c7ec6de
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;
    
    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    
    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;
<<<<<<< HEAD

    //code for getting Unity components
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
=======
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        //grab references
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
>>>>>>> a79e5f80da289a271d05feb990e4633d4c7ec6de
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
<<<<<<< HEAD
        horizontalInput = Input.GetAxis("Horizontal");
        
        //player movement
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //flipping the player direction via transform
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        
        
=======
        //player movement
        float horizontalInput =  Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        
        //flip player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //set animator parameters
            anim.SetBool("walk", horizontalInput != 0);
            anim.SetBool("grounded",  isGrounded());
            
>>>>>>> a79e5f80da289a271d05feb990e4633d4c7ec6de
        //player jump logic
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        //adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);

        body.gravityScale = 7;
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        
        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

<<<<<<< HEAD
    //check if player is on ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

=======
>>>>>>> a79e5f80da289a271d05feb990e4633d4c7ec6de
    private void Jump()
    {
        if(coyoteCounter <=0 && jumpCounter <= 0) return;
        
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        else
        {
            if(coyoteCounter > 0)
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            else
            {
                if (jumpCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                    jumpCounter--;
                }
            }
        }
    }
<<<<<<< HEAD
=======

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
>>>>>>> a79e5f80da289a271d05feb990e4633d4c7ec6de
}
