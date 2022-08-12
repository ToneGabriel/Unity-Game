using System;
using UnityEngine;

public class StartMenuState : GameManagerState
{
    private StartMenuData _startMenuData;
    private Action _loadData;

    public StartMenuState(GameManager gameManager, GameManagerFiniteStateMachine stateMachine, StartMenuData startMenuData)
        : base(gameManager, stateMachine) 
    {
        _startMenuData = startMenuData;

        _startMenuData.NewGameButton.onClick.AddListener(StartNewGame);
        _startMenuData.LoadGameButton.onClick.AddListener(LoadGame);
        _startMenuData.QuitGameButton.onClick.AddListener(QuitGame);
    }

    public override void Enter()
    {
        base.Enter();

        _startMenuData.StartMenuCanvas.SetActive(true);
    }

    public override void LogicUpdate() => base.LogicUpdate();

    public override void Exit()
    {
        base.Exit();

        _startMenuData.StartMenuCanvas.SetActive(false);
    }

    private void StartNewGame()
    {
        _loadData = () => { _gameManager.LoadDataOnNewGame(); };
        _gameManager.LoadingScreenState.SetLoadData(_loadData);
        _gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        _stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }

    private void LoadGame()
    {
        _loadData = () => { SaveManager.Instance.Load(); };
        _gameManager.LoadingScreenState.SetLoadData(_loadData);
        _gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        _stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
