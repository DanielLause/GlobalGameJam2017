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











        //float tempTotal = 0;
        //float remaining = tempTotal;

        //if (FadeBlackView.CanvasGroup.alpha < 1)
        //    tempTotal = 1;

        //float step = FadeStepValue;
        //float waiter = FadeTime / 1;

        //while (remaining > 0)
        //{
        //    tempTotal -= Mathf.Min(remaining, step);
        //    remaining -= step;
        //    yield return new WaitForSeconds(waiter);
        //}
    }
}
