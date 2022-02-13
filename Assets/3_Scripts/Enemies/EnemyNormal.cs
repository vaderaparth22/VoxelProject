using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : Enemy
{
    [Header("Weapon Settings")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform[] shootingPosition;
    [SerializeField] protected float fireSpeed;

    private Vector3 gravityVector;

    private float fireTimerCount;
    private string projectileTag => projectilePrefab.tag;

    protected override void Init()
    {
        base.Init();

        fireTimerCount = Time.time + Random.Range(0, timeBetweenAttacks);
    }

    protected override void Refresh()
    {
        base.Refresh();

        Move();
        Shoot();
    }

    private void Move()
    {
        if (_controller)
        {
            _controller.Move(moveSpeed * Time.deltaTime * PlayerDistanceNormalized);

            if (_controller.isGrounded && gravityVector.y < 0)
            {
                gravityVector.y = 0;
            }
            else
            {
                gravityVector.y += gravityValue;
            }

            _controller.Move(gravityVector * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if(Time.time > fireTimerCount)
        {
            fireTimerCount = Time.time + Random.Range(0, timeBetweenAttacks);

            for (int i = 0; i < shootingPosition.Length; i++)
            {
                GameObject newBullet = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
                newBullet.transform.SetPositionAndRotation(shootingPosition[i].position, shootingPosition[i].localRotation);

                Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
                bulletRb.velocity = Vector3.zero;
                bulletRb.AddForce(shootingPosition[i].transform.forward * fireSpeed);
            }
        }
    }
}
