using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EntityModeState
{
    private EnemyHitMode        _enemyHitMode;
    private EnemyHitModeData    _enemyHitModeData;

    public EnemyHitState(EnemyHitMode mode, EnemyHitModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyHitMode       = mode;
        _enemyHitModeData   = data;
    }


}
