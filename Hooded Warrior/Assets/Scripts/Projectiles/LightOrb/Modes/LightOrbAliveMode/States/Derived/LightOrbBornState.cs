
public sealed class LightOrbBornState : LightOrbBaseState
{
    public LightOrbBornState(LightOrbAliveMode mode, LightOrbAliveModeData data)
        : base(mode, data) { /*Empty*/ }

    public override void Update()
    {
        base.Update();

        if (_lightOrbAliveMode.Target_InnerLightInnerRadius < _lightOrbAliveModeData.InnerLightMaxInnerRadius)
        {
            // gradually grow inner light radius
            _lightOrbAliveMode.Target_InnerLightInnerRadius += _lightOrbAliveModeData.InnerLightInnerRadiusChangeRatio;
            _lightOrbAliveMode.Target_InnerLightOuterRadius += _lightOrbAliveModeData.InnerLightOuterRadiusChangeRatio;
        }
        else if (_lightOrbAliveMode.Target_OuterLightInnerRadius < _lightOrbAliveModeData.OuterLightMaxInnerRadius)
        {
            // gradually grow outer light radius
            _lightOrbAliveMode.Target_OuterLightInnerRadius += _lightOrbAliveModeData.OuterLightInnerRadiusChangeRatio;
            _lightOrbAliveMode.Target_OuterLightOuterRadius += _lightOrbAliveModeData.OuterLightOuterRadiusChangeRatio;
        }
        else
            _lightOrbAliveMode.ChangeState(LightOrbAliveMode.StateID.Live);
    }
}
