
public abstract class LightOrbBaseState : State
{
    protected LightOrbAliveMode     _lightOrbAliveMode;
    protected LightOrbAliveModeData _lightOrbAliveModeData;

    public LightOrbBaseState(LightOrbAliveMode mode, LightOrbAliveModeData data)
    {
        _lightOrbAliveMode      = mode;
        _lightOrbAliveModeData  = data;
    }

    public override void FixedUpdate()
    {
        // Follow target
        _lightOrbAliveMode.Target_MoveTowardsFollowTarget(_lightOrbAliveModeData.SmoothSpeed);
    }
}
