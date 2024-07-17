using System.Collections;
using UnityEngine;

public sealed class GameManagerResetGameState : GameManagerControlBaseState
{
    private ResetGameData _resetGameData;
    private WaitForSeconds _timeBeforeDeathPrompt;

    public GameManagerResetGameState(GameManagerControlMode mode, ResetGameData resetGameData)
        : base(mode)
    {
        _resetGameData = resetGameData;
        _timeBeforeDeathPrompt = new WaitForSeconds(2f);

        _resetGameData.ResetToLoadGameButton.onClick.AddListener(ResetToLoadGame);
        _resetGameData.ResetToMainMenuButton.onClick.AddListener(ResetToMainMenu);
    }

    public override void Enter()
    {
        base.Enter();

        //_gameManager.StartCoroutine(ShowDeathPrompt());
    }

    public override void Update() => base.Update();

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
        //_loadData = () => { SaveManager.Instance.Load(); };
        //_gameManager.LoadingScreenState.SetLoadData(_loadData);
        //_gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        //_stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }

    private void ResetToMainMenu()
    {
        //_loadData = null;
        //_gameManager.LoadingScreenState.SetLoadData(_loadData);
        //_gameManager.LoadingScreenState.SetNextState(_gameManager.StartMenuState);
        //_stateMachine.ChangeState(_gameManager.LoadingScreenState);
    }
    #endregion
}
