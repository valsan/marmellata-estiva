using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Debuff : ScriptableObject
{
  public string Name;
  public string EffectDescription;
  public Sprite Icon;

  public UnityEvent OnApply;
  public UnityEvent OnRestore;
  public bool IsActive;
  public virtual void Apply()
  {
    OnApply.Invoke();
    IsActive = true;
  }

  public virtual void Restore()
  {
    OnRestore.Invoke();
    IsActive = false;
  }
}
