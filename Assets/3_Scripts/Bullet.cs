using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] protected GameObject projectileParticle;
    [SerializeField] protected GameObject muzzlePrefab;
    [SerializeField] protected GameObject impactPrefab;

    private bool isUsed;

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
        projectileParticle.transform.parent = transform;
        if (muzzlePrefab)
        {
            muzzlePrefab = Instantiate(muzzlePrefab, transform.position, transform.rotation);
            Destroy(muzzlePrefab, 1.5f);
        }
    }

    private void Refresh()
    {
        if(transform.position.sqrMagnitude > 1000)
            Destroy(gameObject);
    }

    private void FixedRefresh()
    {
        
    }

    private void OnHit(Collision collision) 
    {
        GameObject impactP = Instantiate(impactPrefab, transform.position, Quaternion.identity);

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
                                                                             
        for (int i = 1; i < trails.Length; i++)
        {
            ParticleSystem trail = trails[i];

            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2f);
            }
        }

        Destroy(projectileParticle, 3f);
        Destroy(impactP, 3.5f);
        Destroy(gameObject);
    }
}
