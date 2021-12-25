using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float recoilTime;
    [SerializeField] private float fireSpeed;

    void Start()
    {
        Init();
    }

    void Update()
    {
        Refresh();
    }

    private void FixedUpdate()
    {
        FixedRefresh();
    }

    protected virtual void Init()
    {}

    protected virtual void Refresh()
    {}

    protected virtual void FixedRefresh()
    {}
}
