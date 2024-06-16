using UnityEngine;

public sealed class EnemyPatrolMode : FiniteStateMachine
{
    [SerializeField] private Enemy                _target;
    [SerializeField] private EnemyPatrolModeData  _patrolData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); }}

    protected override void Awake()
    {
        CreateStateArray((int)StateID.Count);

        AddNewState((int)StateID.Idle,           new EnemyIdleState(_target, _patrolData, AnimatorParameters.Idle_b));
        AddNewState((int)StateID.Move,           new EnemyMoveState(_target, _patrolData, AnimatorParameters.Move_b));
        AddNewState((int)StateID.PlayerDetected, new EnemyPlayerDetectedState(_target, _patrolData, AnimatorParameters.PlayerDetected_b));

        SetDefaultState((int)StateID.Move);
    }

    public enum StateID
    {
        Idle,
        Move,
        PlayerDetected,

        Count
    }

    private static class AnimatorParameters
    {
        public static readonly string Idle_b            = "Idle_b";
        public static readonly string Move_b            = "Move_b";
        public static readonly string PlayerDetected_b  = "PlayerDetected_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    Idle_b,
                                    Move_b,
                                    PlayerDetected_b
                                };
        }
    }
}