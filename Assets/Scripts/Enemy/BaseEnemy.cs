using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int Health;
    // resistance to being pushed back by an attack
    public int KnockbackResistance;
    public bool HeavyAttack = false;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() 
    {
    
    }

    public void TakeDamage(int damageTaken, Vector2 direction) 
    {
        Health -= damageTaken;
        if (Health <= 0)
        {
            Die();
        }
        else 
        {
            // Get Knocked Back
            body.AddForce(new Vector2());
        }
    }
}
