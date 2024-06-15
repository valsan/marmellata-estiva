using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBarUI : MonoBehaviour
{
    [SerializeField] private FloatValue _batteryValue;
    [SerializeField] private Image _batteryFill;
    private void OnEnable()
    {
        _batteryValue.OnValueChanged += OnBatteryValueChanged;
    }

    private void OnDisable()
    {
        _batteryValue.OnValueChanged -= OnBatteryValueChanged;
    }

    private void OnBatteryValueChanged()
    {
        _batteryFill.fillAmount = _batteryValue.Value / 100.0f;
    }
}
