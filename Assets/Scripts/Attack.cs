using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage;
    public Vector2 Direction;
    public float Knockback;
    private float LifeSpan;

    public void Spawn(Transform attacker)
    {
        transform.right = attacker.position - transform.position;
        Direction = (transform.position - attacker.position).normalized;
        LifeSpan = GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Destroy(gameObject, LifeSpan);
    }
}
