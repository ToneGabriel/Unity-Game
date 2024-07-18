using System.Collections;
using UnityEngine;


public sealed class GameManagerResetGameState : GameManagerControlBaseState
{
    private ResetGameData   _resetGameData;
    private float           _startTime;
    private float           _waitTime;
    private bool            _deathPromptActive;

    public GameManagerResetGameState(GameManagerControlMode mode, ResetGameData resetGameData)
        : base(mode)
    {
        _resetGameData = resetGameData;

        _resetGameData.ResetToLoadGameButton.onClick.AddListener(ResetToLoadGame);
        _resetGameData.ResetToMainMenuButton.onClick.AddListener(ResetToMainMenu);
    }

    public override void Enter()
    {
        base.Enter();

        _startTime          = Time.time;
        _deathPromptActive  = false;
    }

    public override void Update()
    {
        if (!_deathPromptActive && Time.time >= _startTime + _waitTime)
            _resetGameData.OnDeathCanvas.SetActive(true);
        else
        {
            // wait...
        }
    }

    public override void Exit()
    {
        base.Exit();

        _resetGameData.OnDeathCanvas.SetActive(false);
    }

    #region Reset Functions
    private void ResetToLoadGame()
    {
        //_loadData = () => { SaveManager.Instance.Load(); };
        //_gameManager.LoadingScreenState.SetLoadData(_loadData);

        _gameManagerControlMode.Target_BufferedStateID = GameManagerControlMode.StateID.Gameplay;
        _gameManagerControlMode.ChangeState(GameManagerControlMode.StateID.LoadingScreen);
    }

    private void ResetToMainMenu()
    {
        //_loadData = null;
        //_gameManager.LoadingScreenState.SetLoadData(_loadData);

        _gameManagerControlMode.Target_BufferedStateID = GameManagerControlMode.StateID.StartMenu;
        _gameManagerControlMode.ChangeState(GameManagerControlMode.StateID.LoadingScreen);
    }
    #endregion
}
