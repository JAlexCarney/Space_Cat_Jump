using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseEnemy
{
    private Transform Player;
    private SpriteRenderer SpriteRenderer;

    public new void Start()
    {
        base.Start();
        Player = GameObject.Find("Player").transform;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        // look at player
        if (Player.position.x < transform.position.x)
        {
            SpriteRenderer.flipX = true;
        }
        else 
        {
            SpriteRenderer.flipX = false;
        }
    }
}
