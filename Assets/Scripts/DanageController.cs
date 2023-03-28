using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanageController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioSource damageSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            damageSoundEffect.Play();
            if (healthBar.ChangeHealth(-1)) 
            {
                // player is dead
                Debug.Log("Player Is Dead");
            }
        }
    }
}
