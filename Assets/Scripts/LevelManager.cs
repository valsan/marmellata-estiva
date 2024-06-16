using System;
using System.Collections;
using System.Collections.Generic;
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

    public GameState CurrentState { get; private set; } = GameState.Playing;
    [SerializeField] private FloatValue _currentAmount;
    private void Awake()
    {
        _player.transform.position = _startPosition.position;
    }

    private void Start()
    {
        _canvas.LevelNameText.text = SceneManager.GetActiveScene().name;
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
        DisablePlayerInput();
        _canvas.GameOverScreen.gameObject.SetActive(true);
        _canvas.GameOverScreen.AnimateIn();
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
