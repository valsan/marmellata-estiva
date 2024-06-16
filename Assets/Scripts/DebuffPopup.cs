using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class DebuffPopup : MonoBehaviour
{
  [SerializeField] private Image _icon;
  [SerializeField] private TextMeshProUGUI _title;
  [SerializeField] private TextMeshProUGUI _description;
  [SerializeField] private Image _backgroundImage;
  [SerializeField] private Transform _targetPositionIn;
  [SerializeField] private Transform _targetPositionOut;
  [SerializeField] private float _animationDuration = 3.0f;
  private Coroutine _coroutine;

  private void Awake()
  {
    Hide();
  }

  public void Show(Debuff debuff)
  {
    _title.text = debuff.Name;
    _description.text = debuff.EffectDescription;
    _icon.sprite = debuff.Icon;
    if (_coroutine != null)
    {
      StopCoroutine(_coroutine);
      _backgroundImage.transform.DOMove(_targetPositionOut.position, 0.0f);
    }
    _coroutine = StartCoroutine(AnimateInOut());
  }

  IEnumerator AnimateInOut()
  {
    _backgroundImage.transform.DOMove(_targetPositionIn.position, 0.2f);
    yield return new WaitForSeconds(_animationDuration);
    Hide();
  }

  private void Hide()
  {
    _backgroundImage.transform.DOMove(_targetPositionOut.position, 0.2f);
  }
}
