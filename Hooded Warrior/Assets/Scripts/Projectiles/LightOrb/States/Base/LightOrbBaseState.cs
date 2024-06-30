using UnityEngine;

public abstract class LightOrbBaseState : State
{
    protected LightOrb _lightOrb;

    public LightOrbBaseState(LightOrb lightOrb)
    {
        _lightOrb = lightOrb;
    }

    public override void FixedUpdate()
    {
        // Follow target
        _lightOrb.MoveTowardsTarget(_lightOrb.Data.SmoothSpeed);
    }
}
