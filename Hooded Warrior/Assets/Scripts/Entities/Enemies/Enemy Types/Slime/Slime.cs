using UnityEngine;

public class Slime : Enemy
{
    public Slime_IdleState IdleState { get; private set; }
    public Slime_MoveState MoveState { get; private set; }
    public Slime_PlayerDetectedState PlayerDetectedState { get; private set; }

    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;

    protected override void OnEnable()
    {
        base.OnEnable();

        MoveState = new Slime_MoveState(this, _stateMachine, "walk", _moveStateData);
        IdleState = new Slime_IdleState(this, _stateMachine, "idle", _idleStateData);
        PlayerDetectedState = new Slime_PlayerDetectedState(this, _stateMachine, "playerDetected", _playerDetectedStateData);

        _stateMachine.Initialize(MoveState);
    }

}
