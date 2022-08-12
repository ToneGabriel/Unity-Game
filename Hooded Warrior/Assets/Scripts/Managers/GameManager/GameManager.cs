using UnityEngine;

public enum Scenes
{
    Tutorial,
    Forest,
    Cave
}

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
    public GameManagerFiniteStateMachine StateMachine { get; private set; }
    public StartMenuState StartMenuState { get; private set; }
    public LoadingScreenState LoadingScreenState { get; private set; }
    public GameplayState GameplayState { get; private set; }
    public ResetGameState ResetGameState { get; private set; }

    public bool IsGamePaused;                                       // True when game is paused
    public bool IsLoadingData;                                      // True when loading screen is active
    #endregion

    #region Unity functions
    private void Awake()                                            // Singleton instance
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            InitializeStates();
        }
    }

    private void Start()                                            // Set Game States
    {
        StateMachine.Initialize(StartMenuState);
    }
    
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    #endregion

    #region Other
    public void LoadDataOnNewGame()
    {
        _loadingScreenData.SceneLoaders[(int)Scenes.Tutorial].IsLoaded = true;
        Player.SetNewGameData();
    }

    private void InitializeStates()
    {
        StateMachine = new GameManagerFiniteStateMachine();
        StartMenuState = new StartMenuState(Instance, StateMachine, _startMenuData);
        LoadingScreenState = new LoadingScreenState(Instance, StateMachine, _loadingScreenData);
        GameplayState = new GameplayState(Instance, StateMachine, _gameplayData);
        ResetGameState = new ResetGameState(Instance, StateMachine, _resetGameData);
    }
    #endregion
}