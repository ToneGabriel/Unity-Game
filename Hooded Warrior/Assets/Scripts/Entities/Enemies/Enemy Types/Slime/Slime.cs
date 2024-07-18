using System;
using System.Collections.Generic;
using UnityEngine;


public sealed class Slime : Enemy
{
    public enum StateID
    {
        Patrol,
        Aggro,
        Hit
    }

    public override string[] AnimatorParameterNames
    {
        get
        {
            return Helpers.ConcatArrays
            (
                EnemyPatrolMode.AnimatorParameters.GetAnimatorParameterNames(),
                SlimeAggroMode.AnimatorParameters.GetAnimatorParameterNames(),
                EnemyHitMode.AnimatorParameters.GetAnimatorParameterNames()
            );
        }
    }

    private FiniteStateMachine<StateID> _slimeController = null;

    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;

    protected override void Awake()
    {
        base.Awake();

        _slimeController = new FiniteStateMachine<StateID>();

        _slimeController.InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Patrol,    new EnemyPatrolMode(this, null)),
            new KeyValuePair<StateID, State>(StateID.Aggro,     new SlimeAggroMode(this, null)),
            new KeyValuePair<StateID, State>(StateID.Hit,       new EnemyHitMode(this, null))
        );

        _slimeController.SetDefaultState(StateID.Patrol);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    #region Controller Interface
    protected override State GetControlState()
    {
        return _slimeController;
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
        if (_slimeController.IsStateActive(StateID.Patrol))
            _slimeController.ChangeState(StateID.Aggro);
        else if (_slimeController.IsStateActive(StateID.Aggro))
            _slimeController.ChangeState(StateID.Patrol);
        else if (_slimeController.IsStateActive(StateID.Hit))
            _slimeController.ChangeState(StateID.Aggro);
    }
    #endregion Controller Interface
}
