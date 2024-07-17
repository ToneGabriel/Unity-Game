using System;
using System.Collections.Generic;
using UnityEngine;


public sealed class GameManager : MonoBehaviourController
{
    public enum StateID
    {
        Control
    }

    public static GameManager Instance;     // Singleton instance

    private FiniteStateMachine<StateID> _gameManagerController;

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
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;

            // the rest of Awake
            base.Awake();

            _gameManagerController = new FiniteStateMachine<StateID>();

            _gameManagerController.InitializeStates
            (
                new KeyValuePair<StateID, State>(StateID.Control, new GameManagerControlMode(Instance))
            );

            _gameManagerController.SetDefaultState(StateID.Control);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
    #endregion Unity functions

    #region Controller Interface
    protected override State GetControlState()
    {
        return _gameManagerController;
    }

    protected override bool UpdateConditions()
    {
        return true;
    }

    protected override bool FixedUpdateConditions()
    {
        return true;
    }

    public override void ChangeState()
    {
        // only 1 mode
        throw new NotImplementedException("Not intended for implementation!");
    }
    #endregion Controller Interface

    #region Other
    public void LoadDataOnNewGame()
    {
        _loadingScreenData.SceneLoaders[(int)SceneNames.Scene1_TutorialScene].IsLoaded = true;
        Player.SetNewGameData();
    }

    //protected override void FSMInitializeModes()
    //{
    //    //AddNewMode(0);

    //    //AddNewState(0, (int)GameManagerStateID.StartMenu,      new StartMenuState(Instance, _startMenuData));
    //    //AddNewState(0, (int)GameManagerStateID.LoadingScreen,  new LoadingScreenState(Instance, _loadingScreenData));
    //    //AddNewState(0, (int)GameManagerStateID.Gameplay,       new GameplayState(Instance, _gameplayData));
    //    //AddNewState(0, (int)GameManagerStateID.ResetGame,      new ResetGameState(Instance, _resetGameData));
    //}
    #endregion Other
}