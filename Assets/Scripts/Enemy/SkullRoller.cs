using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRoller : BaseEnemy
{
    private Transform Player;
    private bool facingRight = false;
    private Rigidbody2D body;
    private Animator animator;
    private float maxSpeed = 5f;

    public new void Start()
    {
        base.Start();
        Player = GameObject.Find("Player").transform;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        // look at player
        if (Player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            facingRight = false;
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            facingRight = true;
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("Hurt")) return;

        if (facingRight && body.velocity.x < maxSpeed) 
        {
            // move right
            body.AddForce(new Vector2(8f, 0f));
        } 
        else if (!facingRight && body.velocity.x > -maxSpeed)
        {
            // move left
            body.AddForce(new Vector2(-8f, 0f));
        }
    }
}
