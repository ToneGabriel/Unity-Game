using UnityEngine;

public sealed class GameManager : FSMMonoBehaviour
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
    public bool IsGamePaused { get; set; }                  // True when game is paused
    public bool IsLoadingData { get; set; }                 // True when loading screen is active
    #endregion

    #region Unity functions
    protected override void Awake()                         // Singleton instance
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;

            base.Awake();   // Init here due to singleton
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }
    #endregion

    #region Other
    public void LoadDataOnNewGame()
    {
        _loadingScreenData.SceneLoaders[(int)SceneNames.Scene1_TutorialScene].IsLoaded = true;
        Player.SetNewGameData();
    }

    protected override void FSMInitializeStates()
    {
        AddNewState((int)GameManagerStateID.StartMenu,      new StartMenuState(Instance, _startMenuData));
        AddNewState((int)GameManagerStateID.LoadingScreen,  new LoadingScreenState(Instance, _loadingScreenData));
        AddNewState((int)GameManagerStateID.Gameplay,       new GameplayState(Instance, _gameplayData));
        AddNewState((int)GameManagerStateID.ResetGame,      new ResetGameState(Instance, _resetGameData));
    }

    protected override void FSMInitializeTransitions()
    {
        throw new System.NotImplementedException();
    }

    protected override bool FSMUpdateConditions()
    {
        // always update
        return true;
    }

    protected override bool FSMFixedUpdateConditions()
    {
        // always update
        return true;
    }
    #endregion Other
}