using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDrill : BaseEnemy
{
    public Transform endPointTransform;
    private bool facingRight = false;
    private Rigidbody2D body;
    private Animator animator;
    private float maxSpeed = 5f;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool headingToEnd;

    public new void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
        endPos = endPointTransform.position;
    }
    // Update is called once per frame
    private void LookAtGoal()
    {
        // look at player
        if (headingToEnd)
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

    private void UpdateGoal() 
    {
        if (headingToEnd && transform.position.x > endPos.x)
        {
            body.velocity = new Vector2();
            headingToEnd = false;
        } 
        else if (!headingToEnd && transform.position.x < startPos.x) 
        {
            body.velocity = new Vector2();
            headingToEnd = true;
        }
    }

    private void FixedUpdate()
    {
        LookAtGoal();
        UpdateGoal();

        if (animator.GetBool("Hurt")) return;

        if (!facingRight && body.velocity.x < maxSpeed)
        {
            // move right
            body.AddForce(new Vector2(8f, 0f));
        }
        else if (facingRight && body.velocity.x > -maxSpeed)
        {
            // move left
            body.AddForce(new Vector2(-8f, 0f));
        }
    }
}
