using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffsCollection : MonoBehaviour
{
    [field: SerializeField] public List<Debuff> AvailableDebuffs { get; private set; }= new();
    public List<Debuff> ActiveBuffs { get; private set; } = new();
}
