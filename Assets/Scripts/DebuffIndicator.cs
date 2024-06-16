using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffIndicator : MonoBehaviour
{
  [SerializeField] Debuff _debuff;
  private Image _icon;

  private void Awake()
  {
    _icon = GetComponent<Image>();
  }

  private void Start()
  {
    _icon.sprite = _debuff.Icon;
    _icon.enabled = _debuff.IsActive;
  }

  private void Update()
  {
    _icon.enabled = _debuff.IsActive;
  }
}
