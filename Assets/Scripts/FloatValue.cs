using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject
{
   [SerializeField] private float _value;
   public float Value
   {
      get => _value;
      set
      {
         _value = value;
         OnValueChanged?.Invoke();
      }
   }

   public Action OnValueChanged;
}
