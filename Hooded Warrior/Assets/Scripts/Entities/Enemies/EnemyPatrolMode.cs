using UnityEngine;

public sealed class EnemyPatrolMode : FiniteStateMachine
{
    [SerializeField] private Enemy                  _target;
    [SerializeField] private EnemyPatrolModeData    _patrolData;

    public override string[] AnimatorParameterNames
    {
        get
        {
            return new string[] {   "Idle_b" ,
                                    "Move_b",
                                    "PlayerDetected_b" };
        }
    }

    protected override void Awake()
    {
        string[] animParams = AnimatorParameterNames;

        CreateStateArray((int)EnemyPatrolModeStateID.Count);

        AddNewState((int)EnemyPatrolModeStateID.Idle,           new EnemyIdleState(_target, animParams[0]));
        AddNewState((int)EnemyPatrolModeStateID.Move,           new EnemyMoveState(_target, animParams[1]));
        AddNewState((int)EnemyPatrolModeStateID.PlayerDetected, new EnemyPlayerDetectedState(_target, animParams[2]));

        SetDefaultState((int)EnemyPatrolModeStateID.Move);
    }
}

public enum EnemyPatrolModeStateID
{
    Idle,
    Move,
    PlayerDetected,

    Count
}