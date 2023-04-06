using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float currentSpeed = 0f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deceleration = 1f;
    [SerializeField] private float skidDeceleration = 2f;
    [SerializeField] private float maxMoveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        var deltaX = dirX * acceleration;
        if (IsGrounded())
        {
            if (deltaX == 0f && currentSpeed != 0)
            {
                // Decelerate
                currentSpeed += -1f * Mathf.Sign(currentSpeed) * deceleration;
            }
            else if (deltaX != 0f && currentSpeed != 0 && Mathf.Sign(currentSpeed) != Mathf.Sign(deltaX))
            {
                // Skid
                currentSpeed += deltaX * skidDeceleration;
            }
            else
            {
                // Accelerating
                currentSpeed += deltaX;
            }
        }
        else 
        {
            currentSpeed += deltaX;
        }
        

        currentSpeed = Mathf.Clamp(currentSpeed, -maxMoveSpeed, maxMoveSpeed);
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        UpdateAnimationState();
    }

    private void Update()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(currentSpeed, jumpForce);
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
