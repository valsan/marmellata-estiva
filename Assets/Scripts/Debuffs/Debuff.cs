using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff : ScriptableObject
{
   public string Name;
   public string EffectDescription;
   public virtual void Apply(){}
   public virtual void Restore(){}
}
