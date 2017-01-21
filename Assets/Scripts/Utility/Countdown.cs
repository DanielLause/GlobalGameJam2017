using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : UnitySingleton<Countdown> {

    public delegate void CountdownExpired();
    public event CountdownExpired OnCountDownExpired;

    [Header("Reference")]
    public Text CountdownText;

    public bool Paused { get; set; }

    private int seconds;
    private Coroutine timer;

    public void StartCountdown(int seconds)
    {
        this.seconds = seconds;
        CountdownText.text = string.Format("{0}:{1:00}", seconds / 60, seconds % 60);
        timer = StartCoroutine(Timer());
    }

    public void PauseCountdown()
    {
        StopCoroutine(timer);
        Paused = true;
    }
    
    public void UnpauseCountdown()
    {
        timer = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        Paused = false;
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
