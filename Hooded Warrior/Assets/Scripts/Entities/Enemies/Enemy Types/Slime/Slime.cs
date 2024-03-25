using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;

    protected override void Awake()
    {
        base.Awake();

        InitializeStates();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stateMachine.InitializeState(_states[(int)SlimeStateID.Move]);
    }

    private void InitializeStates()
    {
        _stateMachine   = new FiniteStateMachine();
        _states         = new State[(int)SlimeStateID.Count];

        _states[(int)SlimeStateID.Idle]             = new SlimeIdleState(this, "idle", _idleStateData);
        _states[(int)SlimeStateID.Move]             = new SlimeMoveState(this, "walk", _moveStateData);
        _states[(int)SlimeStateID.PlayerDetected]   = new SlimePlayerDetectedState(this, "playerDetected", _playerDetectedStateData);
    }

}
