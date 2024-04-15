using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Components & Data
    public Player Player;                                           // Reference to player
    public Camera MainCamera;                                       // Reference to Main Camera
    public Transform GameStartPlayerPosition;                       // The position where the player is set at New Game

    [SerializeField] private StartMenuData _startMenuData;
    [SerializeField] private GameplayData _gameplayData;
    [SerializeField] private ResetGameData _resetGameData;
    [SerializeField] private LoadingScreenData _loadingScreenData;
    #endregion

    #region Game States
    private FiniteStateMachine  _stateMachine;
    private State[]             _states;

    //public GameManagerFiniteStateMachine StateMachine { get; private set; }
    //public StartMenuState StartMenuState { get; private set; }
    //public LoadingScreenState LoadingScreenState { get; private set; }
    //public GameplayState GameplayState { get; private set; }
    //public ResetGameState ResetGameState { get; private set; }

    public bool IsGamePaused { get; set; }                                       // True when game is paused
    public bool IsLoadingData { get; set; }                                      // True when loading screen is active
    #endregion

    #region Unity functions
    private void Awake()                                            // Singleton instance
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance        = this;

            _stateMachine   = new FiniteStateMachine();
            _states         = new State[(int)GameManagerStateID.Count];

            _states[(int)GameManagerStateID.StartMenu]      = new StartMenuState(Instance, _startMenuData);
            _states[(int)GameManagerStateID.LoadingScreen]  = new LoadingScreenState(Instance, _loadingScreenData);
            _states[(int)GameManagerStateID.Gameplay]       = new GameplayState(Instance, _gameplayData);
            _states[(int)GameManagerStateID.ResetGame]      = new ResetGameState(Instance, _resetGameData);
        }
    }

    private void Start()                                            // Set Game States
    {
        _stateMachine.SetInitialState(_states[(int)GameManagerStateID.StartMenu]);
    }
    
    private void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
    }
    #endregion

    #region Other
    public void LoadDataOnNewGame()
    {
        _loadingScreenData.SceneLoaders[(int)SceneNames.Scene1_TutorialScene].IsLoaded = true;
        Player.SetNewGameData();
    }

    public void ChangeState(int stateID)
    {
        _stateMachine.ChangeState(_states[stateID]);
    }
    #endregion

    public void ChangeTimeScale(TimeScale value)
    {
        Time.timeScale = (int)value;
    }
}