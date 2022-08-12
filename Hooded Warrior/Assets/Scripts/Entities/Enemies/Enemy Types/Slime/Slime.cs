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

        MoveState = new Slime_MoveState(this, StateMachine, "walk", _moveStateData);
        IdleState = new Slime_IdleState(this, StateMachine, "idle", _idleStateData);
        PlayerDetectedState = new Slime_PlayerDetectedState(this, StateMachine, "playerDetected", _playerDetectedStateData);

        StateMachine.Initialize(MoveState);
    }

}
