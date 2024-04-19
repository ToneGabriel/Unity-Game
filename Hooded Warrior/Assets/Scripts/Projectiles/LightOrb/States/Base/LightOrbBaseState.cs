using UnityEngine;

public abstract class LightOrbBaseState : State
{
    protected LightOrb _lightOrb;
    private float _lastHoverTime;

    public LightOrbBaseState(LightOrb lightOrb)
    {
        _lightOrb       = lightOrb;
        _lastHoverTime  = Time.time;
    }

    public override void PhysicsUpdate()
    {
        MoveAndFlutter();
        Hover();
    }

    private void MoveAndFlutter()
    {
        _lightOrb.MoveTowardsTarget(_lightOrb.Data.SmoothSpeed);

        // Random direction generator. This results in a "flutter" effect
        _lightOrb.ApplyImpulse( _lightOrb.Data.FlutterAcceleration *
                                Time.deltaTime *
                                Random.insideUnitCircle);
    }

    private void Hover()
    {
        // A regular and prominant directional change,
        // resulting in short darting motions around the target position

        if (Time.time >= _lastHoverTime + _lightOrb.Data.HoverTime)
        {
            _lightOrb.Velocity  = Random.insideUnitCircle * _lightOrb.Data.HoverCircleRange;
            _lastHoverTime      = Time.time;
        }
    }
}
