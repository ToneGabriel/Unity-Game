using System;
using System.Collections;
using UnityEngine;

public class ResetGameState : GameManagerState
{
    private ResetGameData _resetGameData;
    private Action _loadData;
    private WaitForSeconds _timeBeforeDeathPrompt;

    public ResetGameState(GameManager gameManager, GameManagerFiniteStateMachine stateMachine, ResetGameData resetGameData)
        : base(gameManager, stateMachine)
    {
        _resetGameData = resetGameData;
        _timeBeforeDeathPrompt = new WaitForSeconds(2f);

        _resetGameData.ResetToLoadGameButton.onClick.AddListener(ResetToLoadGame);
        _resetGameData.ResetToMainMenuButton.onClick.AddListener(ResetToMainMenu);
    }

    public override void Enter()
    {
        base.Enter();

        _gameManager.StartCoroutine(ShowDeathPrompt());
    }

    public override void LogicUpdate() => base.LogicUpdate();

    public override void Exit()
    {
        base.Exit();

        _resetGameData.OnDeathCanvas.SetActive(false);
    }

    #region Reset Functions
    private IEnumerator ShowDeathPrompt()
    {
        yield return _timeBeforeDeathPrompt;

        _resetGameData.OnDeathCanvas.SetActive(true);
    }

    private void ResetToLoadGame()
    {
        _loadData = () => { SaveManager.Instance.Load(); };
        _gameManager.LoadingScreenState.SetLoadData(_loadData);
        _gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        _stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }

    private void ResetToMainMenu()
    {
        _loadData = null;
        _gameManager.LoadingScreenState.SetLoadData(_loadData);
        _gameManager.LoadingScreenState.SetNextState(_gameManager.StartMenuState);
        _stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }
    #endregion
}
