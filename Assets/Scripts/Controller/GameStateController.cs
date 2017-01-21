using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PausedStates { Paused, UnPause };
public enum GameStates { Menu, Game, PausedMenu, GameEnd };

public class GameStateController : UnitySingleton<GameStateController>
{
    public delegate void PausedStateChanged(PausedStates activPausedState);
    public event PausedStateChanged OnPausedStateChanged;

    public delegate void GameStateChanged(GameStates activeGameState);
    public event GameStateChanged OnGameStateChanged;

    public PausedStates PausedState { get; private set; }
    public GameStates GameState { get; private set; }

    void Start()
    {
        SetPausedState(PausedStates.UnPause);
        SetGameState(GameStates.Menu);
    }

    public void SetPausedState(PausedStates pausedState)
    {
        if (PausedState != pausedState)
        {
            PausedState = pausedState;

            if (OnPausedStateChanged != null)
                OnPausedStateChanged(PausedState);
        }
        else
            return;

        switch (PausedState)
        {
            case PausedStates.Paused:
                break;
            case PausedStates.UnPause:
                break;
            default:
                break;
        }
    }
    private void SetGameState(GameStates gameState)
    {
        if (GameState != gameState)
        {
            GameState = gameState;

            if (OnGameStateChanged != null)
                OnGameStateChanged(GameState);
        }
        else
            return;

        switch (GameState)
        {
            case GameStates.Menu:
            case GameStates.PausedMenu:
            case GameStates.GameEnd:
                SetPausedState(PausedStates.Paused);
                break;
            case GameStates.Game:
                SetPausedState(PausedStates.UnPause);
                break;
            default:
                break;
        }
    }
}
