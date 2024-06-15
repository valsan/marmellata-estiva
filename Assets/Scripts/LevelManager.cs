using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private bool _isLastLevel;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private LevelEndPlatform _levelEnd;

    [SerializeField] private GameplayCanvas _canvas;
    [SerializeField] private FloatValue _charge;
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
        _levelEnd.OnLevelEnded += OnLevelComplete;
    }

    private void OnDisable()
    {
        _levelEnd.OnLevelEnded -= OnLevelComplete;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + (_levelNumber + 1));
    }

    private void OnLevelComplete()
    {
        if (!_isLastLevel)
        {
            LoadNextLevel();
        }
    }

}
