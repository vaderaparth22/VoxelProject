using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : Enemy
{
    private Vector3 gravityVector;

    protected override void Refresh()
    {
        base.Refresh();

        if(_controller)
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
}
