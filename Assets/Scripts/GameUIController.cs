using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : UnitySingleton<GameUIController>
{

    [Header("Reference")]
    public FadeBlackView FadeBlackView;

    [Header("Settings")]
    [SerializeField]
    private float fadeSpeed;

    void Start()
    {
        SceneController.Instance.OnSceneChanged += OnSceneChanged;
        Countdown.Instance.OnCountDownExpired += OnCountdownExpired;
    }

    private void OnCountdownExpired()
    {
        SceneController.Instance.LoadScene(SceneType.Menu);
    }

    private void OnSceneChanged(SceneType e)
    {
        if (e== SceneType.Game)
        {
            Countdown.Instance.StartCountdown(10);
        }
    }

    public void Fade()
    {
        StartCoroutine(FadeProgress());
    }

    private IEnumerator FadeProgress()
    {
        float canvasAlpha = FadeBlackView.CanvasGroup.alpha;

        if (canvasAlpha > 0.9f)
        {
            while (FadeBlackView.CanvasGroup.alpha != 0)
            {
                FadeBlackView.CanvasGroup.alpha -= (fadeSpeed / 10);
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        else
            while (FadeBlackView.CanvasGroup.alpha != 1)
            {
                FadeBlackView.CanvasGroup.alpha += (fadeSpeed /10);
                yield return new WaitForSecondsRealtime(0.01f);
            }
    }
}
