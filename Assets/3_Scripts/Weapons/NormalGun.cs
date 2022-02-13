using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : Weapon
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void Refresh()
    {
        base.Refresh();
    }

    protected override void FixedRefresh()
    {
        base.FixedRefresh();
    }

    protected override void Fire()
    {
        //GameObject newBullet = Instantiate(projectilePrefab, shootingPosition.position, shootingPosition.localRotation);

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
