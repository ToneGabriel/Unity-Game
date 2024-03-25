using UnityEngine;

public class ArcherDeadState : EnemyDeadState
{
    private Archer _archer;

    public ArcherDeadState(Archer archer, string animBoolName, Data_Dead stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_stateData.DeathBloodParticle, _enemy.transform.position, _stateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_stateData.DeathChunkParticle, _enemy.transform.position, _stateData.DeathChunkParticle.transform.rotation);
    }
}
