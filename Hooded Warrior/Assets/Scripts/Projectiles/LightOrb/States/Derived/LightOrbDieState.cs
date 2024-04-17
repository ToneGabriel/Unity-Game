using UnityEngine;

public class LightOrbDieState : LightOrbBaseState
{
    public LightOrbDieState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_lightOrb.IsOrbLightAtZeroRadius())
            _lightOrb.Die();
        else
            _lightOrb.DecreaseLightRadius();
    }
}
