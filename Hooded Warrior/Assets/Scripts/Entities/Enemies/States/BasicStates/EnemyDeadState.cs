using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected EnemyStateData _stateData;

    public EnemyDeadState(Enemy enemy, string animBoolName, EnemyStateData stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_stateData.DeathBloodParticle, _enemy.transform.position, _stateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_stateData.DeathChunkParticle, _enemy.transform.position, _stateData.DeathChunkParticle.transform.rotation);
    }

    public virtual void FinishDeathAnimation()              // Called on death animation frame
    {
        _enemy.gameObject.SetActive(false);
    }

}
