using System;
using System.Collections.Generic;
using UnityEngine;


public sealed class LightOrbAliveMode : FiniteStateMachine<LightOrbAliveMode.StateID>
{
    public enum StateID
    {
        Born,
        Live,
        Die
    }

    private readonly LightOrb               _target;
    private readonly LightOrbAliveModeData  _aliveData;

    public LightOrbAliveMode(LightOrb target, LightOrbAliveModeData aliveData)
    {
        _target     = target;
        _aliveData  = aliveData;

        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Born,  new LightOrbBornState(this, _aliveData)),
            new KeyValuePair<StateID, State>(StateID.Live,  new LightOrbLiveState(this, _aliveData)),
            new KeyValuePair<StateID, State>(StateID.Die,   new LightOrbDieState(this, _aliveData))
        );

        SetDefaultState(StateID.Born);
    }

    #region Properties
    public float Target_InnerLightInnerRadius
    {
        get { return _target.InnerLight.pointLightInnerRadius; }
        set { _target.InnerLight.pointLightInnerRadius = value; }
    }

    public float Target_InnerLightOuterRadius
    {
        get { return _target.InnerLight.pointLightOuterRadius; }
        set { _target.InnerLight.pointLightOuterRadius = value; }
    }

    public float Target_OuterLightInnerRadius
    {
        get { return _target.OuterLight.pointLightInnerRadius; }
        set { _target.OuterLight.pointLightInnerRadius = value; }
    }

    public float Target_OuterLightOuterRadius
    {
        get { return _target.OuterLight.pointLightOuterRadius; }
        set { _target.OuterLight.pointLightOuterRadius = value; }
    }

    public Vector2 Target_Velocity
    {
        get { return _target.Rigidbody.velocity; }
        set { _target.Rigidbody.velocity = value; }
    }
    #endregion Properties

    #region Other
    public void Target_MoveTowardsFollowTarget(float speed)
    {
        if (_target.FollowTarget != null)
            _target.transform.position = Vector3.Lerp(_target.transform.position, _target.FollowTarget.transform.position, speed);
        else
        {
            // wait for target to be set...
        }
    }

    public void Target_ApplyImpulse(Vector2 impulse)
    {
        _target.Rigidbody.AddForce(impulse, ForceMode2D.Impulse);
    }

    public void Target_Die()
    {
        ObjectPoolManager.Instance.ReturnToPool(_target);
    }
    #endregion Other
}
