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
        GameObject newBullet = ObjectPooler.SharedInstance.GetPooledObject("Missile");
        newBullet.SetActive(true);
        newBullet.transform.SetPositionAndRotation(shootingPosition.position, shootingPosition.localRotation);

        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.velocity = Vector3.zero;
        bulletRb.AddForce(shootingPosition.transform.forward * fireSpeed);
    }
}
