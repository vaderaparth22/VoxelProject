using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{

    #region SINGLETON
    private static EnemyManager instance;

    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyManager();
            }
            return instance;
        }
    }
    #endregion

    private EnemyInfo enemyInfo;

    public void Init(EnemyInfo enemyInfo)
    {
        this.enemyInfo = enemyInfo;
        SpawnMultiple(enemyInfo.startSpawnAmount);
    }

    private void SpawnMultiple(int amount)
    {
        int posCounter = 0;

        for (int i = 0; i < amount; i++)
        {
            Enemy e = ObjectPooler.SharedInstance.GetPooledObject("EnemyNormal").GetComponent<Enemy>();
            enemyInfo.listOfLiveEnemies.Add(e);
            e.gameObject.SetActive(true);

            e.transform.position = enemyInfo.spawningPositions[posCounter].position;
            posCounter++;
        }
    }

    public void SpawnNewEnemy()
    {

    }
}
