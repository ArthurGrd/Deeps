using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public float radiusCollisionGround;
    public LayerMask layerDetect;
    public Animator animator;
    public SpriteRenderer sr;
 
    private bool isJumping;
    private bool isGrounded;
    private float horizontalMovement;
    private Vector3 _velocity = Vector3.zero;
    
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        } 
        Flip(rb.velocity.x);
        float chatracterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed",chatracterVelocity);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCollisionGround,layerDetect);
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
    }
    
    void MovePlayer(float _horizontalMovement)
    {
        var velocity1 = rb.velocity;
        Vector3 targetVelocity = new Vector2(_horizontalMovement, velocity1.y);
        rb.velocity = Vector3.SmoothDamp(velocity1, targetVelocity,ref _velocity,.05f);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f,jumpForce));
            isJumping = false;
        }
    }

    void Flip(float velocity)
    {
        if (velocity > 1.1f)
        {
            sr.flipX = false;
        }
        else if (velocity < -1.1f)
        {
            sr.flipX = true;
        }
    }

    public void ResetMovement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
