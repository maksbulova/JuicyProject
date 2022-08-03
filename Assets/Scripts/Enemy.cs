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
    [SerializeField] private MMF_Player landFeedback;
    [SerializeField] private MMF_Player hitFeedback;
    [SerializeField] private MMF_Player deathFeedback;
    [SerializeField] private float knockbackPower = 10;
    [SerializeField] private Vector3 additinoalKnockback = Vector3.up;
    [SerializeField] private ShockWaveSource swSource;
    [SerializeField] private float minVelocity = 0.01f;

    private float previousFallSpeed = 0.1f;
    private bool isJumping = true;

    public Collider Collider => enemyCollider;

    public event System.Action<Enemy> OnDeath;

    private void FixedUpdate()
    {
        if (isJumping)
        {
            HandleFall();
        }
    }

    public void RecieveHit(BulletHitInfo bulletHitInfo)
    {
        Vector3 force = enemyRB.mass * enemyRB.drag * knockbackPower * bulletHitInfo.push + additinoalKnockback;
        enemyRB.AddForce(force);
        // isJumping = true;
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
        swSource.GenerateShoke();

        OnDeath?.Invoke(this);

        // Destroy(gameObject);
    }

    private void HandleFall()
    {
        // landed
        if (Mathf.Abs(enemyRB.velocity.y) < minVelocity && previousFallSpeed < minVelocity)
        {
            isJumping = false;
            landFeedback.PlayFeedbacks();
        }

        previousFallSpeed = Mathf.Abs(enemyRB.velocity.y);
    }

}
