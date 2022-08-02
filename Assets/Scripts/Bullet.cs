using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private LayerMask hittableLayers;

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
    }

    private void Hit(Collider hitObject)
    {
        BulletHitInfo hitInfo = new BulletHitInfo(bulletRB.velocity * bulletRB.mass, damage);
        SceneController.Instance.EnemyController.OnEnemyHit(hitObject, hitInfo);

        // Feedback

        Destroy(gameObject);
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