using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithMinions : Enemy
{
    [SerializeField] private GameObject minionChildPrefab;
    [SerializeField] private int numOfChild;
    [SerializeField] private float childSpawnRadius;
    [SerializeField] private float childSpawnDelay;

    [Space]
    [SerializeField] private LayerMask projectileLayer;
    [SerializeField] private LayerMask mainPlayerLayer;

    private float spawnTimer;
    private int totalSpawned;

    protected override void Init()
    {
        base.Init();

        spawnTimer = Time.time;
        totalSpawned = numOfChild;
    }

    protected override void Refresh()
    {
        base.Refresh();

        if(spawnTimer > Time.time && totalSpawned <= numOfChild)
        {
            SpawnNewChild();
        }
    }

    protected override void OnGetHit(Collider other)
    {
        if((projectileLayer | (1 << other.gameObject.layer)) == projectileLayer)
        {
            //Projectiles hit
            UpdateCurrentHealth(other);
        }
        
        if((mainPlayerLayer | (1 << other.gameObject.layer)) == mainPlayerLayer)
        {
            //Player hit
            ApplyDamageToPlayer(other);
        }
    }

    private void UpdateCurrentHealth(Collider other)
    {
        if(health <= 0)
        {
            totalSpawned = 0;
            spawnTimer = childSpawnDelay + Time.time;
        }
    }

    private void ApplyDamageToPlayer(Collider other)
    {

    }

    private void SpawnNewChild()
    {
        totalSpawned++;

        if(totalSpawned > numOfChild)
            Destroy(gameObject);

        spawnTimer = childSpawnDelay + Time.time;

        GameObject c = Instantiate(minionChildPrefab, transform.position, transform.rotation);
        c.transform.position = Random.insideUnitSphere * childSpawnRadius;
    }
}
