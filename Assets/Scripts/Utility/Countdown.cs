using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : UnitySingleton<Countdown> {

    [Header("Reference")]
    public Text CountdownText;

    private int seconds;
    public bool sdsd;

    public bool Paused;

    void Start()
    {
        StartCountdown(120);
    }

    void Update()
    {
        if (sdsd)
        {
            sdsd = false;
            Paused = true;
            PauseTimer();
        }
    }
    public void StartCountdown(int seconds)
    {
        this.seconds = seconds;
        CountdownText.text = string.Format("{0}:{1:00}", seconds / 60, seconds % 60);
        StartCoroutine("Timer");
    }

    public void PauseTimer()
    {
        StopCoroutine("Timer");
    }

    private IEnumerator Timer()
    {
        if (!Paused)
        {
        yield return new WaitForSeconds(1);
        seconds--;
        CountdownText.text = string.Format("{0}:{1:00}", seconds / 60, seconds % 60);
        if (seconds == 0)
            Debug.Log("EVENT");
        else
            StartCoroutine(Timer());
        }
    }
}
