using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    public Dictionary<string, GameObject> pooledDictionary = new Dictionary<string, GameObject>();

    public UnityEvent onPoolFinished;

    public void Init()
    {
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        onPoolFinished.Invoke();
    }

    public void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(true);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}
