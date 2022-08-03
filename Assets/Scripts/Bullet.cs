using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;


public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private LayerMask hittableLayers;

    [Header("VFX")]
    [SerializeField] private MMF_Player flyFeedback;
    [SerializeField] private MMF_Player hitFeedback;

    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        bool targetHittable = hittableLayers == (hittableLayers | (1 << other.gameObject.layer));
        if (targetHittable)
        {
            Hit(other);
        }

    }

    public void Init(Vector3 direction, float velocity, float damage)
    {
        bulletRB.velocity = direction * velocity;
        this.damage = damage;

        flyFeedback.Initialization();
        flyFeedback.PlayFeedbacks();
    }

    private void Hit(Collider hitObject)
    {
        BulletHitInfo hitInfo = new BulletHitInfo(bulletRB.velocity * bulletRB.mass, damage);
        SceneController.Instance.EnemyController.OnEnemyHit(hitObject, hitInfo);

        flyFeedback.StopFeedbacks();
        hitFeedback.Initialization();
        hitFeedback.PlayFeedbacks(transform.position);
    }
}

public struct BulletHitInfo
{
    public Vector3 push;
    public float damage;

    public BulletHitInfo(Vector3 push, float damage)
    {
        this.push = push;
        this.damage = damage;
    }
}