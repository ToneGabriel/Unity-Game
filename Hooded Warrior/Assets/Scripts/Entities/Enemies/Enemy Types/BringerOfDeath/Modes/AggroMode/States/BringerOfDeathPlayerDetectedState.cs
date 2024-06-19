using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathPlayerDetectedState : EntityModeState
{
    private BringerOfDeathAggroMode     _bringerOfDeathAggroMode;
    private BringerOfDeathAggroModeData _bringerOfDeathAggroModeData;

    public BringerOfDeathPlayerDetectedState(BringerOfDeathAggroMode mode, BringerOfDeathAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _bringerOfDeathAggroMode        = mode;
        _bringerOfDeathAggroModeData    = data;
    }
}
