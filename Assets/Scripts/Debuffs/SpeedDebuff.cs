using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Debuffs/SpeedDebuff")]
public class SpeedDebuff : Debuff
{
  private float _initialMaxXWalkVelocity;
  private float _initialMaxXFlyVelocity;

  public override void Apply()
  {
    base.Apply();
    var player = FindObjectOfType<Player>();
    if (player)
    {
      _initialMaxXWalkVelocity = player._maxXWalkVelocity;
      _initialMaxXFlyVelocity = player._maxXFlyVelocity;
      player._maxXWalkVelocity = player._maxXWalkVelocity / 2;
      player._maxXFlyVelocity = player._maxXFlyVelocity / 2;
    }
  }

  public override void Restore()
  {
    base.Restore();
    var player = FindObjectOfType<Player>();
    if (player)
    {
      player._maxXWalkVelocity = _initialMaxXWalkVelocity;
      player._maxXFlyVelocity = _initialMaxXFlyVelocity;
    }
  }
}
