using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class GameManagerControlMode : FiniteStateMachine<GameManagerControlMode.StateID>
{
    public enum StateID
    {
        StartMenu,
        LoadingScreen,
        Gameplay,
        ResetGame
    }

    private readonly GameManager _target;

    public GameManagerControlMode(GameManager target)
    {
        _target = target;

        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.StartMenu,     new GameManagerStartMenuState(this, null)),
            new KeyValuePair<StateID, State>(StateID.LoadingScreen, new GameManagerLoadingScreenState(this, null)),
            new KeyValuePair<StateID, State>(StateID.Gameplay,      new GameManagerGameplayState(this, null)),
            new KeyValuePair<StateID, State>(StateID.ResetGame,     new GameManagerResetGameState(this, null))
        );

        SetDefaultState(StateID.StartMenu);
    }

    public StateID Target_BufferedStateID
    {
        get;
        set;
    }
}
