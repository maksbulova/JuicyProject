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

        hitFeedback.PlayFeedbacks();

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        deathFeedback.PlayFeedbacks();

        OnDeath?.Invoke(this);

        // Destroy(gameObject);
    }
}
