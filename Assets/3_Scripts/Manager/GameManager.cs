using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyInfo enemyInfo;

    private ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = Instantiate<ObjectPooler>(Resources.Load<ObjectPooler>("Prefabs/ObjectPooler"));
        EnemyManager.Instance.Init(this.enemyInfo);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}

[System.Serializable]
public class EnemyInfo
{
    public int startSpawnAmount;
    public Transform[] spawningPositions;
    public List<Enemy> listOfLiveEnemies;
}
