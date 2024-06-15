using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Debuffs/SpeedDebuff")]
public class SpeedDebuff : Debuff
{
    private float _moveSpeedInitialValue;
    public override void Apply()
    {
        base.Apply();
        var player = FindObjectOfType<Player>();
        if (player)
        {
            _moveSpeedInitialValue = player._moveSpeed;
            player._moveSpeed /= 2;
        }
    }

    public override void Restore()
    {
        base.Restore();
        var player = FindObjectOfType<Player>();
        if (player)
        {
            player._moveSpeed = _moveSpeedInitialValue;
        }
    }
}
