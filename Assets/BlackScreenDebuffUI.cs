using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlackScreenDebuffUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private BlackScreenDebuff _blackScreenDebuff;
    private TweenerCore<Color, Color, ColorOptions> _tween;

    private void OnEnable()
    {
        _blackScreenDebuff.OnApply.AddListener(StartAnimation);
    }

    private void OnDisable()
    {
        _blackScreenDebuff.OnApply.RemoveListener(StartAnimation);
    }

    private void StartAnimation()
    {
        _tween?.Complete();
        _tween  = _backgroundImage.DOFade(0.8f, 2.0f).SetLoops(-1, LoopType.Yoyo);
    }
}
