using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioSource damageSoundEffect;

    [SerializeField] private GameObject DeathAnimation;

    private BoxCollider2D currentRespawnAnchor;
    private Rigidbody2D rb;
    private PlayerMovement pm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collided with Enemy HurtBox OR World Hazard
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 7) 
        {
            damageSoundEffect.Play();
            if (healthBar.ChangeHealth(-1))
            {
                // Player is Dead
                Instantiate(DeathAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject.GetComponent<SpriteRenderer>());
                Destroy(pm);
                Destroy(this);
                return;
            }
        }
        // collided with Enemy HurtBox
        if (collision.gameObject.layer == 12)
        {
            var dir = (transform.position - collision.transform.position).normalized;
            pm.KnockBack(dir * 10);
        }
        // collided with world Hazard
        if (collision.gameObject.layer == 7)
        {
            transform.position = currentRespawnAnchor.transform.position 
                - new Vector3(0f, currentRespawnAnchor.size.y / 2f, 0f)
                + new Vector3(0f, GetComponent<BoxCollider2D>().size.y/2f, 0f);
        }
        if (collision.gameObject.CompareTag("RespawnAnchor"))
        {
            currentRespawnAnchor = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }
}
