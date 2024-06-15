using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/InvertedControlsDebuff")]
public class InvertedControlsDebuff : Debuff
{
    public bool AreControlsInverted = false;
    public override void Apply()
    {
        base.Apply();
        AreControlsInverted = true;
    }

    public override void Restore()
    {
        base.Restore();
        AreControlsInverted = false;
    }
}
