using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int opals = 0;

    [SerializeField] private Text coinsText;

    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Opal"))
        {
            collectionSoundEffect.Play();
            opals += collision.gameObject.GetComponent<Opal>().value;
            Destroy(collision.transform.parent.gameObject);
            coinsText.text = "x" + opals;
        }
    }
}
