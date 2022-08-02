using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 1;

    private Dictionary<Collider, Enemy> enemyDictionary;

    private void Start()
    {
        enemyDictionary = new Dictionary<Collider, Enemy>();

        StartCoroutine(DelaySpawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemyDictionary.Add(newEnemy.Collider, newEnemy);

        newEnemy.OnDeath += EnemyOnDeath;
    }

    private void EnemyOnDeath(Enemy deadEnemy)
    {
        enemyDictionary.Remove(deadEnemy.Collider);
        StartCoroutine(DelaySpawn());
    }

    public void OnEnemyHit(Collider enemyCollider, BulletHitInfo bulletHitInfo)
    {
        if (enemyDictionary.ContainsKey(enemyCollider))
        {
            enemyDictionary[enemyCollider].RecieveHit(bulletHitInfo);
        }
    }
}
