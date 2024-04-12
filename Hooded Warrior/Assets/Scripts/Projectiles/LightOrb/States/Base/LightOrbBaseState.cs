using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightOrbBaseState : State
{
    protected LightOrb _lightOrb;

    public LightOrbBaseState(LightOrb lightOrb)
    {
        _lightOrb = lightOrb;
    }

    public override void LogicUpdate()
    {
        _lightOrb.CheckOrbTime();
    }

    public override void PhysicsUpdate()
    {
        _lightOrb.UpdatePosition();
        _lightOrb.UpdateHoverDirection();
    }
}
