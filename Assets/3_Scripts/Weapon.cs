using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform shootingPosition;
    [SerializeField] protected float recoilTime;
    [SerializeField] protected float fireSpeed;
    [SerializeField] protected float damage;

    protected MainPlayer player;

    private float nextFireTimer;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Refresh();
    }

    private void FixedUpdate()
    {
        FixedRefresh();
    }

    protected virtual void Refresh() 
    {
        nextFireTimer += Time.deltaTime;

        if(player.GetLookVector != Vector2.zero)
        {
            if(nextFireTimer >= recoilTime)
            {
                Fire();
                nextFireTimer = 0;
            }
        }
    }
    protected virtual void FixedRefresh() { }
    protected virtual void Init()
    {
        player = transform.root.GetComponent<MainPlayer>();
    }

    protected virtual void Fire() { }
}
