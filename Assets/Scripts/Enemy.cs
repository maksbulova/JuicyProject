using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 3;
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Rigidbody enemyRB;

    [Header("VFX")]
    [SerializeField] private MMF_Player hitFeedback;
    [SerializeField] private MMF_Player deathFeedback;
    [SerializeField] private float knockbackPower = 100;
    [SerializeField] private Vector3 additinoalKnockback = Vector3.up;


    public Collider Collider => enemyCollider;

    public event System.Action<Enemy> OnDeath;

    public void RecieveHit(BulletHitInfo bulletHitInfo)
    {
        Vector3 force = enemyRB.mass * enemyRB.drag * knockbackPower * bulletHitInfo.push + additinoalKnockback;
        enemyRB.AddForce(force);
        RecieveDamage(bulletHitInfo.damage);
    }

    private void RecieveDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Death();
        }
        else
        {
            hitFeedback.PlayFeedbacks();
        }
    }

    private void Death()
    {
        deathFeedback.PlayFeedbacks();

        OnDeath?.Invoke(this);

        // Destroy(gameObject);
    }
}
