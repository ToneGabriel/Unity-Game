using UnityEngine;

public class EnemyDeadState : EntityModeState
{
    private EnemyHitMode        _enemyHitMode;
    private EnemyHitModeData    _enemyHitModeData;

    public EnemyDeadState(EnemyHitMode mode, EnemyHitModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyHitMode       = mode;
        _enemyHitModeData   = data;
    }

    public override void Enter()
    {
        base.Enter();

        //GameObject.Instantiate(_enemyHitModeData.DeathBloodParticle, _enemyHitMode.transform.position, _enemyHitModeData.DeathBloodParticle.transform.rotation);
        //GameObject.Instantiate(_enemyHitModeData.DeathChunkParticle, _enemyHitMode.transform.position, _enemyHitModeData.DeathChunkParticle.transform.rotation);
    }

    public virtual void FinishDeathAnimation()              // Called on death animation frame
    {
        //_enemy.gameObject.SetActive(false);
    }

}
