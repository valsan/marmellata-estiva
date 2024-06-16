using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class Charge : MonoBehaviour
{
    [SerializeField] private float _initialAmount;
    [SerializeField] private float _consumptionRate;

    [SerializeField] private FloatValue _currentAmount;
    [SerializeField] private List<Debuff> _availableDebuffs = new();
    private List<Debuff> _activeDebuffs = new();

    [field: SerializeField] public int DesiredMaxNumberOfDebuffs;

    private float _previousAmount;

    [SerializeField] private LevelManager _levelManager;
    
    private void Awake()
    {
        _previousAmount = _currentAmount.Value;
        _currentAmount.Value = _initialAmount; // CHANGE THIS IN PRODUCTION :)
    }

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
        if (_previousAmount / _initialAmount > 0.9f && _currentAmount.Value / _initialAmount <= 0.9f)
        {
            ApplyRandomDebuff();
        }

        _previousAmount = _currentAmount.Value;
    }

    private void Update()
    {
        if (_levelManager.CurrentState == GameState.Playing)
        {
            _currentAmount.Value -= _consumptionRate * Time.deltaTime;
        }
    }

    private void ApplyRandomDebuff()
    {
        if (_availableDebuffs.Count > 0)
        {
            int randomIndex = Random.Range(0, _activeDebuffs.Count - 1);
            _availableDebuffs[randomIndex].Apply();
            _levelManager._canvas.DebuffPopup.Show( _availableDebuffs[randomIndex]);
            // _activeDebuffs.Add(_activeDebuffs[randomIndex]);
            // _availableDebuffs.Remove(_availableDebuffs[randomIndex]);
        }
    }
    
    private void RemoveRandomDebuff()
    {
        if (_activeDebuffs.Count > 0)
        {
            var last = _activeDebuffs.Last();
            last.Restore();
            _availableDebuffs.Add(last);
            _activeDebuffs.Remove(last);
        }
    }
}
