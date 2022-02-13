using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyInfo enemyInfo;

    private ObjectPooler objectPooler;

    private void Awake()
    {
        InitializeObjectPooler();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void InitializeObjectPooler()
    {
        objectPooler = Instantiate<ObjectPooler>(Resources.Load<ObjectPooler>("Prefabs/ObjectPooler"));

        objectPooler.onPoolFinished.AddListener(() => EnemyManager.Instance.Init(this.enemyInfo));
        objectPooler.Init();
    }
}

[System.Serializable]
public class EnemyInfo
{
    public int startSpawnAmount;
    public Transform spawnLocationParent;
    public List<Enemy> listOfLiveEnemies;
}
