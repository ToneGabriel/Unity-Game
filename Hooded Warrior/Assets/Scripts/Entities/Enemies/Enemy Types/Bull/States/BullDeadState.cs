using UnityEngine;

public class BullDeadState : EnemyDeadState
{
    private Bull _bull;

    public BullDeadState(Bull bull, string animBoolName, Data_Dead stateData) 
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_stateData.DeathBloodParticle, _enemy.transform.position, _stateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_stateData.DeathChunkParticle, _enemy.transform.position, _stateData.DeathChunkParticle.transform.rotation);
    }
}
