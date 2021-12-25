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
        GameObject bullet = Instantiate(projectilePrefab, shootingPosition.position, shootingPosition.localRotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * fireSpeed);
    }
}
