using UnityEngine;

public class EnemyDeadState : EnemyState
{

    public EnemyDeadState(Enemy enemy, string animBoolName) 
        : base(enemy, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(_enemy.NonAggroStateData.DeathBloodParticle, _enemy.transform.position, _enemy.NonAggroStateData.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(_enemy.NonAggroStateData.DeathChunkParticle, _enemy.transform.position, _enemy.NonAggroStateData.DeathChunkParticle.transform.rotation);
    }

    public virtual void FinishDeathAnimation()              // Called on death animation frame
    {
        _enemy.gameObject.SetActive(false);
    }

}
