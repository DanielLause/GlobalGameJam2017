using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : UnitySingleton<Countdown>
{

    public delegate void CountdownExpired();
    public event CountdownExpired OnCountDownExpired;

    [Header("Reference")]
    public Text CountdownText;

    public bool IsActive { get; set; }

    private int seconds;
    private Coroutine timer;
    private bool blockedByPausedState;

    void Awake()
    {
        GameStateController.Instance.OnPausedStateChanged += OnPausedStateChanged;
    }

    private void OnPausedStateChanged(PausedStates activPausedState)
    {
        if (activPausedState == PausedStates.Paused && IsActive)
        {
            PauseCountdown();
            blockedByPausedState = true;
        }
        else if (activPausedState == PausedStates.UnPause && blockedByPausedState)
        {
            UnpauseCountdown();
        }
    }

    public void StartCountdown(int seconds)
    {
        this.seconds = seconds;
        CountdownText.text = string.Format("{0}:{1:00}", seconds / 60, seconds % 60);
        timer = StartCoroutine(Timer());
    }

    public void PauseCountdown()
    {
        StopCoroutine(timer);
        IsActive = false;
    }

    public void UnpauseCountdown()
    {
        timer = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        IsActive = true;
        yield return new WaitForSeconds(1);
        seconds--;
        CountdownText.text = string.Format("{0}:{1:00}", seconds / 60, seconds % 60);
        if (seconds == 0)
        {
            if (OnCountDownExpired != null)
                OnCountDownExpired();
        }
        else
            timer = StartCoroutine(Timer());
    }
}
