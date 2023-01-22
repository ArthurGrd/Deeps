using System;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Componants")]
    [SerializeField]private Rigidbody2D rb;

    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Transform checkGroud;
    [SerializeField] public float radius;
    
    [Header("Movement Variables")]
    [SerializeField]private float _movementAcceleration = 50f;
    [SerializeField]private float _maxMoveSpeed= 12f;
    [SerializeField]private float _linearDrag = 10f;
    
    [Header("Jump Variables")] 
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallmult = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;

    [Header("Layer Mask")] 
    [SerializeField] private LayerMask _groundLayer;

    [Header("Ground Collision Variable")] 
    [SerializeField] private float _groundRaycastLength;

    private bool _onGround;
    
    private float _horizontalDirection;
    private bool _changeDirection => (rb.velocity.x > 0f && _horizontalDirection < 0f) || (rb.velocity.x < 0f && _horizontalDirection > 0f);


    private bool canJump => Input.GetButtonDown("Jump") && _onGround;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _horizontalDirection = GetInput().x;
        CheckCollisions();
        if (Input.GetButtonDown("Jump") && _onGround) 
            Jump();
        if (_onGround)
        {
            ApplyLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultplier();
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        ApplyLinearDrag();
        Flip(rb.velocity.x);
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = _airLinearDrag;
    }

    private void FallMultplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = _fallmult;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }


    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(_horizontalDirection,0f) * _movementAcceleration);
        if (Mathf.Abs(rb.velocity.x) > _maxMoveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * _maxMoveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changeDirection)
        {
            rb.drag = _linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void CheckCollisions()
    {
        _onGround = Physics2D.OverlapCircle(checkGroud.position, radius, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkGroud.position,radius);
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
