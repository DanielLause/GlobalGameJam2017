using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedUiController : UnitySingleton<PausedUiController>
{

    private Animator animator;
    GameStateController gamestateController;

    private int fadeInHashAnimation = Animator.StringToHash("FadeIn");
    private int fadeOutHashAnimation = Animator.StringToHash("FadeOut");

    public bool IsActive { get; private set; }
    void Awake()
    {
        animator = GetComponent<Animator>();
        gamestateController = GameStateController.Instance;

        gamestateController.OnGameStateChanged += OnGameStateChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            gamestateController.SetGameState(GameStates.PausedMenu);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gamestateController.SetGameState(GameStates.Game);
        }
    }

    private void OnGameStateChanged(GameStates activeGameState)
    {
        if (activeGameState == GameStates.PausedMenu)
        {
            IsActive = true;
            gamestateController.SetPausedState(PausedStates.Paused);
            animator.SetTrigger(fadeInHashAnimation);

        }
        else if (IsActive && activeGameState == GameStates.Game)
        {
            animator.SetTrigger(fadeOutHashAnimation);
            StartCoroutine(SetUnPauseStateAfterAnimation());
        }
    }

    private IEnumerator SetUnPauseStateAfterAnimation()
    {
        yield return new WaitForSeconds(1);
        IsActive = false;
        gamestateController.SetPausedState(PausedStates.UnPause);
    }
}
