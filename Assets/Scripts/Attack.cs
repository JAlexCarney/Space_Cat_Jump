using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage;
    public Vector2 Direction;
    public float Knockback;
    public float Recoil;
    [HideInInspector]
    public bool isBounce;
    private float LifeSpan;

    public void Spawn(Transform attacker)
    {
        transform.right = attacker.position - transform.position;
        Direction = (transform.position - attacker.position).normalized;
        isBounce = Direction.y < 0;
        LifeSpan = GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Destroy(gameObject, LifeSpan);
    }
}
