using UnityEngine;

public sealed class LightOrbLiveState : LightOrbBaseState
{
    private float _spellCastTime;
    private float _lastHoverTime;

    public LightOrbLiveState(LightOrbAliveMode mode, LightOrbAliveModeData data)
        : base(mode, data) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _spellCastTime = Time.time;
        _lastHoverTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= _spellCastTime + _lightOrbAliveModeData.SpellLifeTime)
            _lightOrbAliveMode.ChangeState(LightOrbAliveMode.StateID.Die);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // Random direction generator. This results in a "flutter" effect
        _lightOrbAliveMode.Target_ApplyImpulse( _lightOrbAliveModeData.FlutterAcceleration *
                                                Time.deltaTime *
                                                Random.insideUnitCircle);

        // A regular and prominant directional change,
        // resulting in short darting motions ("hover") around the target position
        if (Time.time >= _lastHoverTime + _lightOrbAliveModeData.HoverTime)
        {
            _lightOrbAliveMode.Target_Velocity  = _lightOrbAliveModeData.HoverCircleRange * Random.insideUnitCircle;
            _lastHoverTime                      = Time.time;
        }
    }
}
