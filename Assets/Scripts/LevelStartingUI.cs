using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelStartingUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _countdownText;

    private void Awake()
    {
        _countdownText.enabled = false;
    }

    public void Animate()
    {
        StartCoroutine(AnimateCoroutine());
    }

    IEnumerator AnimateCoroutine()
    {
        _countdownText.enabled = true;
        _countdownText.text = "3";
        _countdownText.rectTransform.DOScale(5, 1.0f);
        _countdownText.DOFade(0, 1.0f);
        yield return new WaitForSeconds(1.0f);
        _countdownText.text = "2";
        _countdownText.rectTransform.DOScale(1, 0.0f);
        _countdownText.DOFade(1, 0.0f);
        _countdownText.rectTransform.DOScale(5, 1.0f);
        _countdownText.DOFade(0, 1.0f);
        yield return new WaitForSeconds(1.0f);
        _countdownText.text = "1";
        _countdownText.rectTransform.DOScale(1, 0.0f);
        _countdownText.DOFade(1, 0.0f);
        _countdownText.rectTransform.DOScale(5, 1.0f);
        _countdownText.DOFade(0, 1.0f);
        yield return new WaitForSeconds(1.0f);
        _countdownText.enabled = false;
    }
}
