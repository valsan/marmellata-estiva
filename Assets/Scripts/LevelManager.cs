using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private bool _isLastLevel;

    private void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + _levelNumber + 1);
    }

    public void OnLevelComplete()
    {
        if (!_isLastLevel)
        {
            LoadNextLevel();
        }
    }
}
