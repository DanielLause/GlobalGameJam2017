using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUIController : UnitySingleton<CreditsUIController>
{
    private MenuUIController menuUIController;
    private AudioSource audioSource;
    private CanvasGroup canvasGroup;
    private bool isActive =false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        canvasGroup = GetComponent<CanvasGroup>();
        menuUIController = MenuUIController.Instance;
    }

    public void Enable(bool enable)
    {
        isActive = enable;

        canvasGroup.alpha = enable ? 1 : 0;
    }

    public void OnMouseOferCookieCrewLogo()
    {
        if (!audioSource.isPlaying && isActive)
            audioSource.Play();
    }

    public void OnClickBackButton()
    {
        menuUIController.Enable(true);
        Enable(false);
    }
}
