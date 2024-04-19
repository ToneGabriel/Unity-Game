using UnityEngine;

public sealed class LightOrbLiveState : LightOrbBaseState
{
    private float _spellCastTime;
    private float _lastHoverTime;

    public LightOrbLiveState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void Enter()
    {
        base.Enter();

        _spellCastTime = Time.time;
        _lastHoverTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _spellCastTime + _lightOrb.Data.SpellLifeTime)
            _lightOrb.ChangeState((int)LightOrbStateID.Die);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Random direction generator. This results in a "flutter" effect
        _lightOrb.ApplyImpulse( _lightOrb.Data.FlutterAcceleration *
                                Time.deltaTime *
                                Random.insideUnitCircle);

        // A regular and prominant directional change,
        // resulting in short darting motions ("hover") around the target position
        if (Time.time >= _lastHoverTime + _lightOrb.Data.HoverTime)
        {
            _lightOrb.SetVelocity(  _lightOrb.Data.HoverCircleRange *
                                    Random.insideUnitCircle);
            _lastHoverTime = Time.time;
        }
    }
}
