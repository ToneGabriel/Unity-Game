using UnityEngine;

public sealed class GameManagerStartMenuState : GameManagerControlBaseState
{
    private StartMenuData _startMenuData;

    public GameManagerStartMenuState(GameManagerControlMode mode, StartMenuData startMenuData)
        : base(mode) 
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

    public override void Update() => base.Update();

    public override void Exit()
    {
        base.Exit();

        _startMenuData.StartMenuCanvas.SetActive(false);
    }

    private void StartNewGame()
    {
        //_loadData = () => { _gameManager.LoadDataOnNewGame(); };
        //_gameManager.LoadingScreenState.SetLoadData(_loadData);
        //_gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        //_gameManager.ChangeState((int)GameManagerStateID.LoadingScreen);
    }

    private void LoadGame()
    {
        //_loadData = () => { SaveManager.Instance.Load(); };
        //_gameManager.LoadingScreenState.SetLoadData(_loadData);
        //_gameManager.LoadingScreenState.SetNextState(_gameManager.GameplayState);
        //_gameManager.ChangeState((int)GameManagerStateID.LoadingScreen);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
