using UnityEngine;

public class Bull_DeadState : DeadState
{
    private Bull _bull;

    public Bull_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Dead stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_stateData.DeathBloodParticle, _enemy.transform.position, _stateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_stateData.DeathChunkParticle, _enemy.transform.position, _stateData.DeathChunkParticle.transform.rotation);
    }
}
