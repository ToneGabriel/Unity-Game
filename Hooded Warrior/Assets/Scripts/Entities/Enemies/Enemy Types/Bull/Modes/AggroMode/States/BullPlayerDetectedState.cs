using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullPlayerDetectedState : EntityModeState<BullAggroMode.StateID>
{
    private BullAggroMode       _bullAggroMode;
    private BullAggroModeData   _bullAggroModeData;

    public BullPlayerDetectedState(BullAggroMode mode, BullAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _bullAggroMode      = mode;
        _bullAggroModeData  = data;
    }
}
