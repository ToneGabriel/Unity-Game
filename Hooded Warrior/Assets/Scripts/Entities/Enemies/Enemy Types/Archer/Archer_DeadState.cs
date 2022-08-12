using UnityEngine;

public class Archer_DeadState : DeadState
{
    private Archer _archer;

    public Archer_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Dead stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_stateData.DeathBloodParticle, _enemy.transform.position, _stateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_stateData.DeathChunkParticle, _enemy.transform.position, _stateData.DeathChunkParticle.transform.rotation);
    }
}
