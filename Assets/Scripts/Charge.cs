using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
  public bool _useRandom;

  private void Awake()
  {
    _previousAmount = _currentAmount.Value;
    _currentAmount.Value = _initialAmount; // CHANGE THIS IN PRODUCTION :)
    DesiredMaxNumberOfDebuffs = _availableDebuffs.Count;
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

    int currentDebuffsCount = _activeDebuffs.Count;
    float currentPercentage = _currentAmount.Value / _initialAmount;
    float percentagePerDebuff = 1.0f / (DesiredMaxNumberOfDebuffs + 1);
    int expectedDebuffs = (int)Math.Floor((1 - currentPercentage) / percentagePerDebuff);

    if (expectedDebuffs != currentDebuffsCount)
    {
      if (expectedDebuffs > currentDebuffsCount)
      {
        int diff = expectedDebuffs - currentDebuffsCount;
        for (int i = 0; i < diff; i++)
        {
          ApplyDebuff();
        }
      }

      if (expectedDebuffs < currentDebuffsCount)
      {
        int diff = currentDebuffsCount - expectedDebuffs;
        for (int i = 0; i < diff; i++)
        {

          RemoveRandomDebuff();
        }
      }
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

  private void ApplyDebuff()
  {
    if (_availableDebuffs.Count <= 0) return;
    if (_useRandom)
    {
      int randomIndex = Random.Range(0, _activeDebuffs.Count - 1);
      var randomDebuff = _availableDebuffs[randomIndex];
      randomDebuff.Apply();
      _availableDebuffs.Remove(randomDebuff);
      _activeDebuffs.Add(randomDebuff);
      _levelManager._canvas.DebuffPopup.Show(randomDebuff);
    }
    else
    {
      var lastDebuff = _availableDebuffs.Last();
      lastDebuff.Apply();
      _availableDebuffs.Remove(lastDebuff);
      _activeDebuffs.Add(lastDebuff);
      _levelManager._canvas.DebuffPopup.Show(lastDebuff);
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
