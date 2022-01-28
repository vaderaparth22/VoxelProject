using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType { MELEE, RANGED }

public class Enemy : MonoBehaviour
{
    [Header("Auto-Initialized Runtime")]
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected CharacterController _controller;

    [Header("Movement")]
    [SerializeField] protected bool isUsingPhysics;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotateSpeed;

    [Header("Other")]
    [SerializeField] protected EnemyType type;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float timeBetweenAttacks;

    protected virtual Vector3 PlayerDistance
    {
        get
        {
            Vector3 distance = (target.position - transform.position);
            return distance;
        }
    }

    protected virtual Vector3 PlayerDistanceNormalized
    {
        get
        {
            Vector3 distance = (target.position - transform.position).normalized;
            return distance;
        }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        Refresh();
    }

    void FixedUpdate()
    {
        FixedRefresh();
    }

    protected virtual void Init() 
    {
        target = FindObjectOfType<MainPlayer>().transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(target.position);

        //_controller = GetComponent<CharacterController>();
    }

    protected virtual void Refresh() 
    {
        RotateTowards();
    }

    protected virtual void FixedRefresh() { }

    private void OnTriggerEnter(Collider other)
    {
        OnGetHit(other);
    }
    
    protected virtual void OnGetHit(Collider other) { }

    private void RotateTowards()
    {
        if (target)
        {
            Vector3 distance = PlayerDistanceNormalized;
            distance.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(distance);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
