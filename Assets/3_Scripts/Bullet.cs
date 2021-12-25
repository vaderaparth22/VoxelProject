using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] protected GameObject projectileParticle;
    [SerializeField] protected GameObject muzzlePrefab;
    [SerializeField] protected GameObject impactPrefab;

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

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();

        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        if (muzzlePrefab)
        {
            muzzlePrefab = Instantiate(muzzlePrefab, transform.position, transform.rotation);
            Destroy(muzzlePrefab, 1.5f);
        }
    }

    private void Refresh()
    {}

    private void FixedRefresh()
    {
        Vector3 direction = _rb.velocity;
    }

    private void OnHit(Collision collision) 
    {
        GameObject impactP = Instantiate(impactPrefab, transform.position, Quaternion.identity);
    }
}
