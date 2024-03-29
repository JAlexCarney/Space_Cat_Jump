using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    public static PlayerMovement player;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private float dirY = 0f;
    private float AttackCooldownSeconds = 0.5f;
    private float AttackTimer = 0f;
    private bool facingRight = true;
    private bool knocked = false;
    private int knockedTimer = 0;
    private int knockedCooldown = 30;
    [SerializeField] private float currentSpeed = 0f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deceleration = 1f;
    [SerializeField] private float skidDeceleration = 2f;
    [SerializeField] private float maxMoveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float attackDistance = 0.75f;
    public GameObject BaseAttack;

    private enum MovementState { idle, running, jumping, falling, hurt }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (knocked)
        {
            knockedTimer--;
            if (knockedTimer <= 0) 
            {
                knocked = false;
            }
        }
        else 
        {
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

                // Stop
                if (deltaX == 0f && Mathf.Abs(currentSpeed) < 0.1)
                {
                    currentSpeed = 0f;
                }
            }
            else
            {
                currentSpeed += deltaX;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, -maxMoveSpeed, maxMoveSpeed);
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
        UpdateAnimationState();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if (dirX > 0f) { facingRight = false; } 
        else if (dirX < 0f) { facingRight = true; }
        AttackTimer += Time.deltaTime;

        if (Input.GetButtonDown("Attack") && AttackTimer > AttackCooldownSeconds) 
        {
            Attack();
            AttackTimer = 0f;
        }

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0f, jumpForce);
        }
    }

    private void Attack() 
    {
        Vector3 attackSpawnPosition;

        if (dirY > 0)
        {
            // Attack Up
            attackSpawnPosition = transform.up * attackDistance + transform.position;
        }
        else if (dirY < 0 && !IsGrounded())
        {
            // Attack Down
            attackSpawnPosition = (-transform.up) * attackDistance + transform.position;
        }
        else if (dirX < 0 || (dirX == 0 && facingRight))
        {
            // Attack Right
            attackSpawnPosition = (-transform.right) * attackDistance + transform.position;
        }
        else
        {
            // Attack Left
            attackSpawnPosition = transform.right * attackDistance + transform.position;
        }

        GameObject Attack = Instantiate(BaseAttack, attackSpawnPosition, Quaternion.identity, transform);
        Attack.GetComponent<Attack>().Spawn(transform);
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

        if (knocked)
        {
            state = MovementState.hurt;
        }

        anim.SetInteger("state", (int)state);
        
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Bounce() 
    {
        rb.velocity = new Vector2(0f, jumpForce);
    }

    public void AddForce(Vector2 f)
    {
        rb.AddForce(f);
    }

    public void KnockBack(Vector2 knockBackForce) 
    {
        rb.velocity = new Vector2();
        rb.AddForce(knockBackForce, ForceMode2D.Impulse);
        knocked = true;
        knockedTimer = knockedCooldown;
    }
}
