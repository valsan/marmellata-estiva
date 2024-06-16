using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum GameState
{
  None,
  LoadingLevel,
  Playing,
  GameOver,
}

public class LevelManager : MonoBehaviour
{
  [SerializeField] private int _levelNumber;
  [SerializeField] private bool _isLastLevel;
  [SerializeField] private Player _player;
  [SerializeField] private Transform _startPosition;
  [SerializeField] private LevelEndPlatform _levelEnd;

  [SerializeField] public GameplayCanvas _canvas;
  [SerializeField] private FloatValue _charge;
  [SerializeField] private CinemachineCamera _levelCamera;

  public GameState CurrentState { get; private set; } = GameState.LoadingLevel;
  [SerializeField] private FloatValue _currentAmount;
  [SerializeField] private EventReference _gameOverSFX;

  [SerializeField] private FMODController _fmodController;
  private void Awake()
  {
    _player.transform.position = _startPosition.position;
  }

  private void Start()
  {
    _fmodController.PlayBackgroundMusic();
    _canvas.LevelNameText.text = SceneManager.GetActiveScene().name;
    StartCoroutine(LoadingLevelAnimation());
  }

  IEnumerator LoadingLevelAnimation()
  {
    _levelCamera.Priority.Value = -1;
    _canvas.LevelStartingUI.Animate();
    DisablePlayerInput();
    yield return new WaitForSeconds(3.0f);
    EnablePlayerInput();
    CurrentState = GameState.Playing;
  }

  private void OnEnable()
  {
    _currentAmount.OnValueChanged += OnChargeValueChanged;
    _levelEnd.OnLevelEnded += OnLevelComplete;
  }

  private void OnDisable()
  {
    _currentAmount.OnValueChanged -= OnChargeValueChanged;
    _levelEnd.OnLevelEnded -= OnLevelComplete;
  }

  private void OnChargeValueChanged()
  {
    if (_currentAmount.Value <= 0)
    {
      OnGameOver();
    }
  }

  private void LoadNextLevel()
  {
    SceneManager.LoadScene("Level" + (_levelNumber + 1));
  }

  private void OnGameOver()
  {
    CurrentState = GameState.GameOver;
    _levelEnd.OnLevelEnded -= OnLevelComplete;
    FMODUnity.RuntimeManager.PlayOneShot(_gameOverSFX);
    _fmodController.StopBackgroundMusic();
    DisablePlayerInput();
    _canvas.GameOverScreen.gameObject.SetActive(true);
    _canvas.GameOverScreen.AnimateIn();
    Player player = FindAnyObjectByType<Player>();
    if (player) player.Die();
  }

  private void DisablePlayerInput()
  {
    _player.GetComponent<PlayerInput>().enabled = false;
  }

  private void EnablePlayerInput()
  {
    _player.GetComponent<PlayerInput>().enabled = true;
  }

  private void OnWin()
  {
    CurrentState = GameState.GameOver;
    _levelEnd.OnLevelEnded -= OnLevelComplete;
    DisablePlayerInput();
    _canvas.GameWinScreen.gameObject.SetActive(true);
    _canvas.GameWinScreen.AnimateIn();
  }
  private void OnLevelComplete()
  {
    if (!_isLastLevel)
    {
      LoadNextLevel();
    }
    else
    {
      OnWin();
    }
  }

}
