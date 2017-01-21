using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstromentType { Bass, Flute, Harp, Perc }

public class BackgroundMusicController : UnitySingleton<BackgroundMusicController>
{
    [Header("Game Background MusicFiles")]
    public AudioSource Bass_Music;
    public AudioSource Flute_Music;
    public AudioSource Harp_Music;
    public AudioSource Perc_Music;
    public AudioClip Menü_Music;

    [Header("References")]
    public NodeController Bass_Node;
    public NodeController Flute_Node;
    public NodeController Harp_Node;
    public NodeController Perc_Node;

    private GameStateController gameStateController;

    void Awake()
    {
        gameStateController = GameStateController.Instance;
        gameStateController.OnPausedStateChanged += OnPauseStateChanged;
    }

    private void OnPauseStateChanged(PausedStates activPausedState)
    {
        if (activPausedState == PausedStates.Paused)
        {
            PauseEnable(true);
        }
        else
            PauseEnable(false);
    }

    void Start()
    {
        StartAllGameBackgroundFiles();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Bass_Music.mute = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Flute_Music.mute = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Harp_Music.mute = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Perc_Music.mute = false;
        }


        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            PauseEnable(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            PauseEnable(false);
        }
    }

    private void StartMenuBackgroundFile()
    {

    }
    public void StartAllGameBackgroundFiles()
    {
        Bass_Music.Play();
        Flute_Music.Play();
        Harp_Music.Play();
        Perc_Music.Play();

    }
    private void PauseEnable(bool enable)
    {
        if (enable)
        {
            Bass_Music.Pause();
            Flute_Music.Pause();
            Harp_Music.Pause();
            Perc_Music.Pause();
        }
        else
        {
            Bass_Music.UnPause();
            Flute_Music.UnPause();
            Harp_Music.UnPause();
            Perc_Music.UnPause();
        }

    }
}
