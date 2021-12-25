using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    #region Singleton
    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    #endregion

    Dictionary<Weapon, Stack<IPoolable>> bulletPool = new Dictionary<Weapon, Stack<IPoolable>>();

    public Transform objectPoolParent;

    private ObjectPool()
    {
        objectPoolParent = new GameObject().transform;
        objectPoolParent.name = this.ToString();
    }

    public void AddToPoolStack(Weapon weapon, IPoolable poolable)
    {
        if (!bulletPool.ContainsKey(weapon))
            bulletPool.Add(weapon, new Stack<IPoolable>());

        bulletPool[weapon].Push(poolable);
        poolable.GetBulletGameObject.transform.SetParent(objectPoolParent);
    }

    public void Add(Weapon weaponType, IPoolable poolable)
    {
        if (!bulletPool.ContainsKey(weaponType))
            bulletPool.Add(weaponType, new Stack<IPoolable>());

        bulletPool[weaponType].Push(poolable);
        poolable.GetBulletGameObject.transform.SetParent(objectPoolParent);
        poolable.Pool();
    }

    public IPoolable Retrieve(Weapon weaponType)
    {
        if (bulletPool.ContainsKey(weaponType) && bulletPool[weaponType].Count > 0)
        {
            IPoolable bulletToRet = bulletPool[weaponType].Pop();
            bulletToRet.Depool();
            return bulletToRet;
        }

        return null;
    }
}
