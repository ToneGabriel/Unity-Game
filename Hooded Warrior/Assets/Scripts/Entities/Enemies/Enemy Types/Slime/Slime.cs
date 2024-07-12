using System;
using UnityEngine;


public sealed class Slime : Enemy
{
    public enum StateID
    {
        // TODO: change this for aggro, non-aggro ...
        //Move,
        //Idle,
        //PlayerDetected
    }

    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    //protected override void FSMInitializeModes()
    //{
    //    throw new System.NotImplementedException();
    //    //AddNewState((int)SlimeStateID.Idle,             new SlimeIdleState(this, "idle", _idleStateData));
    //    //AddNewState((int)SlimeStateID.Move,             new SlimeMoveState(this, "walk", _moveStateData));
    //    //AddNewState((int)SlimeStateID.PlayerDetected,   new SlimePlayerDetectedState(this, "playerDetected", _playerDetectedStateData));
    //}

    #region Controller Interface
    protected override State GetControlState()
    {
        return null;    // _fsm
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
        // TODO
    }
    #endregion Controller Interface
}
