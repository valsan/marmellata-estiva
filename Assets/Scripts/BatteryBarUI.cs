using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines.Interpolators;
using UnityEngine.UI;

public class BatteryBarUI : MonoBehaviour
{
    [SerializeField] private FloatValue _batteryValue;
    [SerializeField] private Image _batteryFill;
    private void OnEnable()
    {
        _batteryValue.OnValueChanged += OnBatteryValueChanged;
        OnBatteryValueChanged();
    }

    private void OnDisable()
    {
        _batteryValue.OnValueChanged -= OnBatteryValueChanged;
    }

    private float _targetAmount = 1.0f;
    [SerializeField] private float _progressSpeed =  1.0f;
    private void Update()
    {
        if (_batteryFill)
        {
            _batteryFill.fillAmount =
            Mathf.MoveTowards(_batteryFill.fillAmount, _targetAmount, _progressSpeed * Time.deltaTime);
        }
    }

    private void OnBatteryValueChanged()
    {
        _targetAmount = _batteryValue.Value / 100.0f;
    }
}
