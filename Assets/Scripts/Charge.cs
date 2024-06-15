using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private float _initialAmount;
    [SerializeField] private float _consumptionRate;

    [SerializeField] private FloatValue _currentAmount;

    private List<Debuff> _availableDebuffs;
    private List<Debuff> _activeDebuffs;

    private void OnEnable()
    {
        _currentAmount.OnValueChanged += OnChargeValueChanged;
    }

    private void OnDisable()
    {
        _currentAmount.OnValueChanged -= OnChargeValueChanged;
    }

    private void OnChargeValueChanged()
    {
    }
}
