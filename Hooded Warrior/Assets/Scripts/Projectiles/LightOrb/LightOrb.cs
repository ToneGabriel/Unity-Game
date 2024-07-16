using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[PoolObject]
public sealed class LightOrb : MonoBehaviourController
{
    private enum StateID
    {
        Alive
    }

    #region Components & Data
    [SerializeField] private Light2D                _innerLightComponent;
    [SerializeField] private Light2D                _outerLightComponent;
    [SerializeField] private LightOrbAliveModeData  _aliveModeData;

    public Light2D InnerLight { get { return _innerLightComponent; } }
    public Light2D OuterLight { get { return _outerLightComponent; } }
    public Rigidbody2D Rigidbody { get; private set; }

    public GameObject FollowTarget { get; set; }

    private FiniteStateMachine<StateID> _lightOrbController;
    #endregion Components & Data

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponent<Rigidbody2D>();

        _lightOrbController = new FiniteStateMachine<StateID>();

        _lightOrbController.InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Alive, new LightOrbAliveMode(this, _aliveModeData))
        );

        _lightOrbController.SetDefaultState(StateID.Alive);
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
    #endregion Unity Functions

    #region Controller Interface
    protected override State GetControlState()
    {
        return _lightOrbController;
    }

    protected override bool UpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FixedUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    public override void ChangeState()
    {
        // has only 1 mode
        throw new NotImplementedException("Not intended for implementation!");
    }
    #endregion Controller Interface
}
