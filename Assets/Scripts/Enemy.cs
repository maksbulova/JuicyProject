using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 3;
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Rigidbody enemyRB;

    public Collider Collider => enemyCollider;

    public event System.Action<Enemy> OnDeath;

    public void RecieveHit(BulletHitInfo bulletHitInfo)
    {
        enemyRB.AddForce(bulletHitInfo.push);
        RecieveDamage(bulletHitInfo.damage);
    }

    private void RecieveDamage(float dmg)
    {
        health -= dmg;

        // Feedback

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        // Feedback

        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
