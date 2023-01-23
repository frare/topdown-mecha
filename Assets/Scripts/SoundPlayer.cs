// using FMODUnity;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    /*
    public enum BGMusic
    {
        gameplay,
        menu
    }

    private static FMOD.Studio.EventInstance _eventAmbience;
    private static FMOD.Studio.EventInstance _eventMGameplay;
    private static FMOD.Studio.EventInstance _eventMMenu;

    private void Awake()
    {
        _eventAmbience = RuntimeManager.CreateInstance("event:/BGM/Ambience");
        _eventMGameplay = RuntimeManager.CreateInstance("event:/BGM/BGM_Gameplay");
        _eventMMenu = RuntimeManager.CreateInstance("event:/BGM/BGM_Menu");

        _eventMMenu.start();
        _eventAmbience.start();

        DontDestroyOnLoad(this);
    }

    public static void PlayMusic(BGMusic music)
    {
        switch (music)
        {
            case BGMusic.menu:
                _eventMGameplay.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _eventMMenu.start();
                break;

            case BGMusic.gameplay:
                _eventMMenu.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                _eventMGameplay.start();
                break;
        }
    }
    */
}