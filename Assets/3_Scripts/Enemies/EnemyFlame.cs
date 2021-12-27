using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlame : Enemy
{
    protected override void Refresh()
    {
        base.Refresh();

        if(_controller)
        {
            _controller.Move(moveSpeed * Time.deltaTime * transform.forward);
        }
    }
}
