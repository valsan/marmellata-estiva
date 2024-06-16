using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
   [field: SerializeField] public Image _backgroundImage;
   [field: SerializeField] public TextMeshProUGUI _gameOverText;
   [field: SerializeField] public Button _backToMenuButton;

   private void OnEnable()
   {
      var temp = _backgroundImage.color;
      temp.a = 0.0f;
      _backgroundImage.color = temp;
      
      temp = _gameOverText.color;
      temp.a = 0.0f;
      _gameOverText.color = temp;
      
      _backToMenuButton.gameObject.SetActive(false);
   }

   public void AnimateIn()
   {
      _backgroundImage.DOFade(1, 2.0f);
      _gameOverText.DOFade(1, 5.0f);
      StartCoroutine(ShowButton());
   }

   IEnumerator ShowButton()
   {
      _backToMenuButton.gameObject.SetActive(false);
      yield return new WaitForSeconds(3.0f);
      _backToMenuButton.gameObject.SetActive(true);
      _backToMenuButton.Select();
   }
}
