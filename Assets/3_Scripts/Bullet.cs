using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] protected ParticleSystem projectileParticle;
    [SerializeField] protected ParticleSystem muzzlePrefab;
    [SerializeField] protected ParticleSystem impactPrefab;
    [SerializeField] protected float timerDelay = 1f;

    private bool isUsed;
    private float deactivateTimer;

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        InitEnable();
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

    private void InitEnable()
    {
        deactivateTimer = timerDelay + Time.time;

        if(projectileParticle) projectileParticle.Play();
        if(muzzlePrefab) muzzlePrefab.Play();
    }

    private void Init()
    {
        
    }

    private void Refresh()
    {
        if(Time.time > deactivateTimer)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedRefresh()
    {
        
    }

    private void OnHit(Collision collision) 
    {
        ParticleSystem impactP = Instantiate(impactPrefab, transform.position, Quaternion.identity);

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

        //Destroy(projectileParticle, 3f);
        //projectileParticle.gameObject.SetActive(false);
        Destroy(impactP, 3.5f);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
