using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerStateList pState;
 
    [Header("X Axis Movement")]
    [SerializeField] float walkSpeed = 25f;
 
    [Space(5)]
 
    [Header("Y Axis Movement")]
    [SerializeField] float jumpSpeed = 45;
    [SerializeField] float fallSpeed = 45;
    [SerializeField] int jumpSteps = 20;
    [SerializeField] int jumpThreshold = 7;
    [Space(5)]

    [Header("Recoil")]
    [SerializeField] int recoilXSteps = 4;
    [SerializeField] int recoilYSteps = 10;
    [SerializeField] float recoilXSpeed = 45;
    [SerializeField] float recoilYSpeed = 45;
    [Space(5)]
 
    [Header("Ground Checking")]
    [SerializeField] Transform groundTransform; //This is supposed to be a transform childed to the player just under their collider.
    [SerializeField] float groundCheckY = 0.2f; //How far on the Y axis the groundcheck Raycast goes.
    [SerializeField] float groundCheckX = 1;//Same as above but for X.
    [SerializeField] LayerMask groundLayer;
    [Space(5)]


    float timeSinceAttack;
    float xAxis;
    float yAxis;
    float grabity;
    int stepsXRecoiled;
    int stepsYRecoiled;
    int stepsJumped = 0;
 
    public Rigidbody2D rb;
    [SerializeField] Animator anim;
 
    // Use this for initialization
    void Start () {
        if(pState == null)
        {
            pState = GetComponent<PlayerStateList>();
        }
 
        rb = GetComponent<Rigidbody2D>();
 
        grabity = rb.gravityScale;
    }
    
    // Update is called once per frame
    public void Update () {
        GetInputs();
        Flip();
        Walk(xAxis);
        float CharacterVelo = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("Speed",CharacterVelo);
        Recoil();
    }
 
    void FixedUpdate()
    {
        if (pState.recoilingX == true && stepsXRecoiled < recoilXSteps)
        {
            stepsXRecoiled++;
        }
        else
        {
            StopRecoilX();
        }
        if (pState.recoilingY == true && stepsYRecoiled < recoilYSteps)
        {
            stepsYRecoiled++;
        }
        else
        {
            StopRecoilY();
        }
        if (Grounded())
        {
            StopRecoilY();
        }
 
        Jump();
    }
 
    void Jump()
    {
        if (pState.jumping)
        {
 
            if (stepsJumped < jumpSteps)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                stepsJumped++;
            }
            else
            {
                StopJumpSlow();
            }
        }
 
        //This limits how fast the player can fall
        //Since platformers generally have increased gravity, you don't want them to fall so fast they clip trough all the floors.
        if (rb.velocity.y < -Mathf.Abs(fallSpeed))
        {        
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
        }
    }
 
    void Walk(float MoveDirection)
    {
        //Rigidbody2D rigidbody2D = rb;
        //float x = MoveDirection * walkSpeed;
        //Vector2 velocity = rb.velocity;
        //rigidbody2D.velocity = new Vector2(x, velocity.y);
        if (!pState.recoilingX)
        {
            rb.velocity = new Vector2(MoveDirection * walkSpeed, rb.velocity.y);
 
            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                pState.walking = true;
            }
            else
            {
                pState.walking = false;
            }
            if (xAxis > 0)
            {
                pState.lookingRight = true;
            }
            else if (xAxis < 0)
            {
                pState.lookingRight = false;
            }
 
            //anim.SetBool("Walking", pState.walking);
        }
 
    }
 
    
    void Recoil()
    {
        //since this is run after Walk, it takes priority, and effects momentum properly.
        if (pState.recoilingX)
        {
            if (pState.lookingRight)
            {
                rb.velocity = new Vector2(-recoilXSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(recoilXSpeed, 0);
            }
        }
        if (pState.recoilingY)
        {
            if (yAxis < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, recoilYSpeed);
                rb.gravityScale = 4;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -recoilYSpeed);
                rb.gravityScale = 4;
            }
 
        }
        else
        {
            rb.gravityScale = 3;
        }
    }
 
    void Flip()
    {
        if (xAxis > 0)
        {
            transform.localScale = new Vector2(2, transform.localScale.y);
        }
        else if (xAxis < 0)
        {
            transform.localScale = new Vector2(-2, transform.localScale.y);
        }
    }
 
    void StopJumpQuick()
    {
        //Stops The player jump immediately, causing them to start falling as soon as the button is released.
        stepsJumped = 0;
        pState.jumping = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }
 
    void StopJumpSlow()
    {
        //stops the jump but lets the player hang in the air for awhile.
        stepsJumped = 0;
        pState.jumping = false;
    }
 
    void StopRecoilX()
    {
        stepsXRecoiled = 0;
        pState.recoilingX = false;
    }
 
    void StopRecoilY()
    {
        stepsYRecoiled = 0;
        pState.recoilingY = false;
    }
 
    public bool Grounded()
    {
        //this does three small raycasts at the specified positions to see if the player is grounded.
        if (Physics2D.Raycast(groundTransform.position, Vector2.down, groundCheckY, groundLayer) || Physics2D.Raycast(groundTransform.position + new Vector3(-groundCheckX, 0), Vector2.down, groundCheckY, groundLayer) || Physics2D.Raycast(groundTransform.position + new Vector3(groundCheckX, 0), Vector2.down, groundCheckY, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GetInputs()
    {
        //WASD/Joystick
        yAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");
 
        //This is essentially just sensitivity.
        if (yAxis > 0.25)
        {
            yAxis = 1;
        }
        else if (yAxis < -0.25)
        {
            yAxis = -1;
        }
        else
        {
            yAxis = 0;
        }
 
        if (xAxis > 0.25)
        {
            xAxis = 1;
        }
        else if (xAxis < -0.25)
        {
            xAxis = -1;
        }
        else
        {
            xAxis = 0;
        }
 
        //anim.SetBool("Grounded", Grounded());
        //anim.SetFloat("YVelocity", rb.velocity.y);
 
        //Jumping
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            pState.jumping = true;
        }
 
        if (!Input.GetButton("Jump") && stepsJumped < jumpSteps && stepsJumped > jumpThreshold && pState.jumping)
        {
            StopJumpQuick();
        }
        else if (!Input.GetButton("Jump") && stepsJumped < jumpThreshold && pState.jumping)
        {
            StopJumpSlow();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(groundTransform.position, new Vector2(groundCheckX, groundCheckY));
 
        Gizmos.DrawLine(groundTransform.position, groundTransform.position + new Vector3(0, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(-groundCheckX, 0), groundTransform.position + new Vector3(-groundCheckX, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(groundCheckX, 0), groundTransform.position + new Vector3(groundCheckX, -groundCheckY));
    }
}
