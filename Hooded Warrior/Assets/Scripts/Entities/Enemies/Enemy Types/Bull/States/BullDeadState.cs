using UnityEngine;

public class BullDeadState : EnemyDeadState
{
    private Bull _bull;

    public BullDeadState(Bull bull, string animBoolName) 
        : base(bull, animBoolName)
    {
        _bull = bull;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_enemy.PatrolData.DeathBloodParticle, _enemy.transform.position, _enemy.PatrolData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_enemy.PatrolData.DeathChunkParticle, _enemy.transform.position, _enemy.PatrolData.DeathChunkParticle.transform.rotation);
    }
}
