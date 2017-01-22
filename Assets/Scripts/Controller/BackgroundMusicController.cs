using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstromentType { Bass, Flute, Harp, Perc }

public class BackgroundMusicController : UnitySingleton<BackgroundMusicController>
{
    [Header("Game Background MusicController")]
    public AudioSource Bass_Music;
    public AudioSource Flute_Music;
    public AudioSource Harp_Music;
    public AudioSource Perc_Music;

    [Header("Menu Background MusicController")]
    public AudioSource Menu_Music;

    [Header("PickUp MusicController")]
    public AudioSource Bass_PickUp;
    public AudioSource Flute_PickUp;
    public AudioSource Harp_PickUp;
    public AudioSource Perc_PickUp;
    public AudioSource God_PickUp;
    public List<AudioClip> GodPickUpSounds;

    [Header("WaveBox")]
    public AudioSource WaveBoxJump_Sound;
    public List<AudioClip> Bass_WaveBoxSounds;
    public List<AudioClip> Flute_WaveBoxSounds;
    public List<AudioClip> Harp_WaveBoxSounds;
    public List<AudioClip> Perc_WaveBoxSounds;

    private GameStateController gameStateController;

    private bool pickUpSoundFinished = false;

    void Awake()
    {
        gameStateController = GameStateController.Instance;
        gameStateController.OnPausedStateChanged += OnPauseStateChanged;
    }

    private void OnPauseStateChanged(PausedStates activPausedState)
    {
        if (activPausedState == PausedStates.Paused)
            PauseEnable(true);
        else
            PauseEnable(false);
    }

    void Start()
    {
        PlayMenuBackgroundMusic();
    }

    public void PlayMenuBackgroundMusic()
    {
        Menu_Music.Play();
    }
    public void StopMenuBackgroundMusic()
    {
        Menu_Music.Stop();
    }

    public void PlayAllGameBackgroundFiles()
    {
        Bass_Music.Play();
        Flute_Music.Play();
        Harp_Music.Play();
        Perc_Music.Play();
    }

    private void PlayRandomGodPickUp()
    {
        int i = Random.Range(0, GodPickUpSounds.Count);
        God_PickUp.clip = GodPickUpSounds[i];
        God_PickUp.Play();
        GodPickUpSounds.Remove(GodPickUpSounds[i]);
    }

    public void PlayPickUpSound(InstromentType instromentType)
    {
       StartCoroutine( PlayPickUp(instromentType));
    }

    private IEnumerator PlayPickUp(InstromentType instromentType)
    {
        PlayRandomGodPickUp();

        yield return new WaitForSeconds(2);

        switch (instromentType)
        {
            case InstromentType.Bass:
                Bass_PickUp.Play();
                break;
            case InstromentType.Flute:
                Flute_PickUp.Play();
                break;
            case InstromentType.Harp:
                Harp_PickUp.Play();
                break;
            case InstromentType.Perc:
                Perc_PickUp.Play();
                break;
            default:
                break;
        }

        PlayInstroment(instromentType);
    }

    public void PlayInstroment(InstromentType instromentType)
    {
        switch (instromentType)
        {
            case InstromentType.Bass:
                Bass_Music.mute = false;
                break;
            case InstromentType.Flute:
                Flute_Music.mute = false;
                break;
            case InstromentType.Harp:
                Harp_Music.mute = false;
                break;
            case InstromentType.Perc:
                Perc_Music.mute = false;
                break;
            default:
                break;
        }
    }

    public void PlayWaveBoxJump(InstromentType instromentType)
    {
        if (WaveBoxJump_Sound.isPlaying)
            WaveBoxJump_Sound.Stop();

        switch (instromentType)
        {
            case InstromentType.Bass:
                WaveBoxJump_Sound.clip = Bass_WaveBoxSounds[Random.Range(0, Bass_WaveBoxSounds.Count)];
                WaveBoxJump_Sound.Play();
                break;
            case InstromentType.Flute:
                WaveBoxJump_Sound.clip = Flute_WaveBoxSounds[Random.Range(0, Flute_WaveBoxSounds.Count)];
                WaveBoxJump_Sound.Play();
                break;
            case InstromentType.Harp:
                WaveBoxJump_Sound.clip = Harp_WaveBoxSounds[Random.Range(0, Harp_WaveBoxSounds.Count)];
                WaveBoxJump_Sound.Play();
                break;
            case InstromentType.Perc:
                WaveBoxJump_Sound.clip = Perc_WaveBoxSounds[Random.Range(0, Perc_WaveBoxSounds.Count)];
                WaveBoxJump_Sound.Play();
                break;
            default:
                break;
        }
    }

    private void PauseEnable(bool enable)
    {
        if (enable)
        {
            Menu_Music.Pause();

            Bass_Music.Pause();
            Flute_Music.Pause();
            Harp_Music.Pause();
            Perc_Music.Pause();

            Bass_PickUp.Pause();
            Flute_PickUp.Pause();
            Harp_PickUp.Pause();
            Perc_PickUp.Pause();

            God_PickUp.Pause();

            WaveBoxJump_Sound.Pause();
        }
        else
        {
            Menu_Music.UnPause();

            Bass_Music.UnPause();
            Flute_Music.UnPause();
            Harp_Music.UnPause();
            Perc_Music.UnPause();

            Bass_PickUp.UnPause();
            Flute_PickUp.UnPause();
            Harp_PickUp.UnPause();
            Perc_PickUp.UnPause();

            God_PickUp.UnPause();

            WaveBoxJump_Sound.UnPause();
        }
    }
}
