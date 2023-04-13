using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioSource damageSoundEffect;

    [SerializeField] private GameObject DeathAnimation;

    private BoxCollider2D currentRespawnAnchor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            damageSoundEffect.Play();
            if (healthBar.ChangeHealth(-1))
            {
                // Player is Dead
                Instantiate(DeathAnimation, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else 
            {
                transform.position = currentRespawnAnchor.transform.position 
                    - new Vector3(0f, currentRespawnAnchor.size.y / 2f, 0f)
                    + new Vector3(0f, GetComponent<BoxCollider2D>().size.y/2f, 0f);
            }
        }
        if (collision.gameObject.CompareTag("RespawnAnchor"))
        {
            currentRespawnAnchor = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }
}
