using UnityEngine;

public sealed class Slime : Enemy
{
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

    protected override void FSMInitializeModes()
    {
        throw new System.NotImplementedException();
        //AddNewState((int)SlimeStateID.Idle,             new SlimeIdleState(this, "idle", _idleStateData));
        //AddNewState((int)SlimeStateID.Move,             new SlimeMoveState(this, "walk", _moveStateData));
        //AddNewState((int)SlimeStateID.PlayerDetected,   new SlimePlayerDetectedState(this, "playerDetected", _playerDetectedStateData));
    }

    protected override bool FSMUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FSMFixedUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }
}
