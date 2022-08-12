using System;
using UnityEngine;

public class GameplayState : GameManagerState
{
    private GameplayData _gameplayData;
    private Action _loadData;
    private bool _pauseGameInput;

    public GameplayState(GameManager gameManager, GameManagerFiniteStateMachine stateMachine, GameplayData gameplayData)
        : base(gameManager, stateMachine)
    {
        _gameplayData = gameplayData;
        
        _gameplayData.PauseResumeGameButton.onClick.AddListener(ResumeGame);
        _gameplayData.PauseSaveGameButton.onClick.AddListener(SaveGame);
        _gameplayData.PauseLoadGameButton.onClick.AddListener(LoadGame);
        _gameplayData.PauseQuitGameButton.onClick.AddListener(QuitToMainMenu);
    }

    public override void Enter()
    {
        base.Enter();

        _gameplayData.GameplayCanvas.SetActive(true);
        _gameManager.Player.gameObject.SetActive(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _pauseGameInput = _gameManager.Player.InputHandler.PauseGameInput;    // check "ESC" input for pause/resume game

        if(_gameManager.Player.IsDead)
            _stateMachine.ChangeState(_gameManager.ResetGameState);
        else if(_pauseGameInput)
        {
            if (!_gameManager.IsGamePaused)
                PauseGame();
            else
                ResumeGame();

            _gameManager.Player.InputHandler.UsePauseGameInput();
        }        
    }

    public override void Exit()
    {
        base.Exit();

        _gameplayData.GameplayCanvas.SetActive(false);
        _gameManager.Player.gameObject.SetActive(false);
    }

    #region Pause Functions
    private void PauseGame()
    {
        _gameManager.IsGamePaused = true;
        Time.timeScale = 0f;
        _gameplayData.PausePrompt.SetActive(true);
    }

    private void ResumeGame()
    {
        _gameManager.IsGamePaused = false;
        Time.timeScale = 1f;
        _gameplayData.PausePrompt.SetActive(false);
    }

    private void SaveGame()
    {
        SaveManager.Instance.Save();
    }

    private void LoadGame()
    {
        ResumeGame();
        _loadData = () => { SaveManager.Instance.Load(); };
        _gameManager.LoadingScreenState.SetLoadData(_loadData);
        _gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        _stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }

    private void QuitToMainMenu()
    {
        ResumeGame();
        _loadData = null;
        _gameManager.LoadingScreenState.SetLoadData(_loadData);
        _gameManager.LoadingScreenState.SetNextState(_gameManager.StartMenuState);
        _stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }
    #endregion
}
