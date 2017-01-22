using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : UnitySingleton<MenuUIController> {

    [Header("References")]
    public Text HighscoreValue;

    private CreditsUIController creditsUI;
    private CanvasGroup canvasGroup;
    private bool isActive = true;

    void Awake()
    {
        creditsUI = CreditsUIController.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        if (Countdown.Instance.IsExpired)
            OnClickCreditsButton();
    }

    public void OnClickStartGameButton()
    {
        BackgroundMusicController.Instance.StopMenuBackgroundMusic();
        BackgroundMusicController.Instance.PlayAllGameBackgroundFiles();
        SceneController.Instance.LoadScene(SceneType.Game);
    }

    public void OnClickCreditsButton()
    {
        creditsUI.Enable(true);
        Enable(false);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    public void Enable(bool enable)
    {
        isActive = enable;

        canvasGroup.alpha = enable ? 1 : 0;
    }
}
