using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int Health;
    // resistance to being pushed back by an attack
    public float KnockbackResistance;
    public bool HeavyAttack = false;
    public Collider2D HurtBox;
    public AudioSource DamageTakenSound;
    public AudioSource DeathSound;
    public GameObject ghost;
    private Rigidbody2D Body;
    private Animator Anim;
    private bool Alive = true;
    public int minReward;
    public int maxReward;

    // Start is called before the first frame update
    public void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    public void ReleaseGhost() 
    {
        Instantiate(ghost, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void DropMoney() 
    {
        int reward = Random.Range(minReward, maxReward);
        int remaining;
        int small = reward % 5;
        remaining = reward / 5;
        int medium = remaining % 5;
        remaining = reward / 5;
        int large = remaining;

        for (int i = 0; i < small; i++) 
        {
            Debug.Log("1");
        }
        for (int i = 0; i < medium; i++)
        {
            Debug.Log("5");
        }
        for (int i = 0; i < large; i++)
        {
            Debug.Log("25");
        }
    }

    public void Die() 
    {
        DeathSound.Play();
        Alive = false;
        HurtBox.enabled = false;
        Anim.SetBool("Dead", true);
        DropMoney();
        Invoke("ReleaseGhost", DeathSound.clip.length);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Alive) { return; }
        Attack attack = collision.GetComponent<Attack>();
        if (attack) 
        {
            TakeDamage(attack);
        }
    }

    public void StopTheHurt() 
    {
        Anim.SetBool("Hurt", false);
    }

    public void TakeDamage(Attack attack) 
    {
        Health -= attack.Damage;
        Anim.SetBool("Hurt", true);

        // Knock Player Back
        if (attack.isBounce)
        {
            // BOING!
            PlayerMovement.player.Bounce();
        }
        else 
        {
            // Apply Recoil to player
            PlayerMovement.player.AddForce(attack.Direction * -200f * attack.Recoil);
        }
        
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("StopTheHurt", 0.75f);
            // Get Knocked Back
            DamageTakenSound.Play();
            Body.AddForce( 200f * (attack.Direction * attack.Knockback) / KnockbackResistance);
        }
    }
}
