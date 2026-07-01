using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    
    private float horizontalInput;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.gravityScale = 7;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // player movement
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // set animator parameters
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);

        // coyote time and jump counter
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
    // player jump logic
    private void Jump()
    {
        if (coyoteCounter <= 0 && jumpCounter <= 0) 
            return;
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        else
        {
            if (coyoteCounter > 0)
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
        
        coyoteCounter = 0;
    }

    // check if player is grounded or not
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );
        return raycastHit.collider != null;
    }
}