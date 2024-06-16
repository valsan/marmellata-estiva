using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering;
using STOP_MODE = FMOD.Studio.STOP_MODE;


[CreateAssetMenu]
public class FMODController : ScriptableObject
{
    [SerializeField] private EventReference _gameplayBackgroundMusic;

    private EventInstance _instance;

    private bool _isPlayingBackgorundMusic;
    // [SerializeField] private EventReference _menuBackgroundMusic;
    public void PlayBackgroundMusic()
    {
        if (!_isPlayingBackgorundMusic)
        {
            _instance = FMODUnity.RuntimeManager.CreateInstance(_gameplayBackgroundMusic);
            _instance.start();
            _isPlayingBackgorundMusic = true;
        }
    }

    public void StopBackgroundMusic()
    {
        if (_instance.hasHandle())
        {
            _isPlayingBackgorundMusic = false;
            _instance.stop(STOP_MODE.IMMEDIATE);
        }
    }
}
