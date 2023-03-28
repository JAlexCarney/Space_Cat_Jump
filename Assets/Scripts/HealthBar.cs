using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int health;
    [SerializeField] private GameObject emptyHeart;
    [SerializeField] private GameObject fullHeart;
    private GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hearts = new GameObject[maxHealth];
        for (int i = 0; i < maxHealth; i++) 
        {
            GameObject newEmptyHeart = Instantiate(emptyHeart, transform);
            newEmptyHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(42f * i, 0f);
            hearts[i] = Instantiate(fullHeart, transform);
            hearts[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(42f * i, 0f);
        }
    }

    public bool ChangeHealth(int dif) 
    {
        health += dif;
        if (health <= 0) 
        {
            // Player is dead
            return true;
        }
        if (health >= maxHealth) 
        {
            health = maxHealth;
        }

        for (int i = 0; i < maxHealth; i++) 
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else 
            {
                hearts[i].SetActive(false);
            }
        }

        // Player is NOT dead
        return false;
    }
}
