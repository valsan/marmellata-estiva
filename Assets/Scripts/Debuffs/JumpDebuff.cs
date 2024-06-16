using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/JumpDebuff")]
public class JumpDebuff : Debuff
{
    [SerializeField] private float _normalJumpForce = 19.0f;
    [SerializeField] private float _debuffedJumpForce = 16.5f;

    public float JumpForce => IsActive ? _debuffedJumpForce : _normalJumpForce;
}
