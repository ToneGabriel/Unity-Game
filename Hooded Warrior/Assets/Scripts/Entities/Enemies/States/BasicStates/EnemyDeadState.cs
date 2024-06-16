using UnityEngine;

public class EnemyDeadState : EnemyPatrolState
{

    public EnemyDeadState(Enemy enemy, string animBoolName) 
        : base(enemy, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_enemy.PatrolData.DeathBloodParticle, _enemy.transform.position, _enemy.PatrolData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_enemy.PatrolData.DeathChunkParticle, _enemy.transform.position, _enemy.PatrolData.DeathChunkParticle.transform.rotation);
    }

    public virtual void FinishDeathAnimation()              // Called on death animation frame
    {
        _enemy.gameObject.SetActive(false);
    }

}
