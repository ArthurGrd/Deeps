using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public PlayerStateList pState;
    [Header("X Axis Movement")]
    [SerializeField] float walkSpeed = 25f;

    public SpriteRenderer SR;
 
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
    [SerializeField] Transform groundTransform; 
    [SerializeField] float groundCheckY = 0.2f; 
    [SerializeField] float groundCheckX = 1;
    [SerializeField] LayerMask groundLayer;
    [Space(5)]
    
    
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 64f;
    private float dashingTime = 0.15f;
    private float dashingCooldown = 0.7f;
    private float lastdash;

    public AudioSource dash;
    public AudioSource running;
    public AudioSource jump;

    [SerializeField] private TrailRenderer tr;


    float timeSinceAttack;
    float xAxis;
    float yAxis;
    float grabity;
    int stepsXRecoiled;
    int stepsYRecoiled;
    int stepsJumped = 0;
    public Slider cooldown;
 
    public Rigidbody2D rb;
    [SerializeField] Animator anim;
 
    void Start () {
        if(pState == null)
        {
            pState = GetComponent<PlayerStateList>();
        }
 
        rb = GetComponent<Rigidbody2D>();
 
        grabity = rb.gravityScale;
    }
    
    public void Update () {
        GetInputs();
        Flip();
        Walk(xAxis);
        float CharacterVelo = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("Speed",CharacterVelo);
        if (!Grounded())
        {
            anim.SetFloat("Speed",0);
        }
        anim.SetBool("isfalling",!Grounded());
        if (!(CharacterVelo > 5 && Grounded()))
        {
            running.Play();
        }
        Recoil();
    }
 
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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
            jump.Play();
        }
        
        if (rb.velocity.y < -Mathf.Abs(fallSpeed))
        {        
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
        }
    }
 
    void Walk(float MoveDirection)
    {
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
 
        }
 
    }
 
    
    void Recoil()
    {
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
            rb.gravityScale = 4;
        }
    }
 
    void Flip()
    {
        if (xAxis > 0)
        {
            transform.localScale = new Vector2(3, transform.localScale.y); 
        }
        else if (xAxis < 0)
        {
            transform.localScale = new Vector2(-3, transform.localScale.y);
        }
    }
 
    void StopJumpQuick()
    {
        stepsJumped = 0;
        pState.jumping = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }
 
    void StopJumpSlow()
    {
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
        yAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");
        float CharacterVelo = Mathf.Abs(rb.velocity.x);
        
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash && CharacterVelo > 10)
        {
            anim.SetTrigger("Dasjh");
            StartCoroutine(Dash());
        }
        
        if (yAxis > 0.10)
        {
            yAxis = 1;
        }
        else if (yAxis < -0.10)
        {
            yAxis = -1;
        }
        else
        {
            yAxis = 0;
        }
 
        if (xAxis > 0.10)
        {
            xAxis = 1;
        }
        else if (xAxis < -0.10)
        {
            xAxis = -1;
        }
        else
        {
            xAxis = 0;
        }
 
 
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
 
        Gizmos.DrawLine(groundTransform.position, groundTransform.position + new Vector3(0, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(-groundCheckX, 0), groundTransform.position + new Vector3(-groundCheckX, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(groundCheckX, 0), groundTransform.position + new Vector3(groundCheckX, -groundCheckY));
    }
    
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        dash.Play();
        walkSpeed = 35;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        walkSpeed = 11;
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        CooldownSlider();
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void CooldownSlider()
    {
        for (float i = 0; i <= dashingCooldown; i+= Time.deltaTime)
        {
            cooldown.value = i / dashingCooldown;
        }
    }
    
}
