using System;
using UnityEngine;


public abstract class EntityMode<EState> : FiniteStateMachine<EState>
where EState : Enum
{
    private readonly Entity _target;
    protected Vector2       _workspaceVector2;

    public EntityMode(Entity entity)
    {
        _target = entity;
    }

    #region Properties
    public abstract string[] AnimatorParameterNames { get; }    // used for animator generation

    public int Target_FacingDirection
    {
        get { return _target.FacingDirection; }
    }

    public Vector2 Target_Position
    {
        get { return _target.transform.position; }
        set { _target.transform.position = value; }
    }

    public Quaternion Target_Rotation
    {
        get { return _target.transform.rotation; }
        set { _target.transform.rotation = value; }
    }

    public Vector2 Target_Velocity
    {
        get { return _target.Rigidbody.velocity; }
        set { _target.Rigidbody.velocity = value; }
    }
    #endregion Properties

    #region Setters
    public void ExitCurrentMode()
    {
        _target.ChangeState();
    }

    public void Target_SetAnimatorBoolParam(string animBoolName, bool value)
    {
        _target.Animator.SetBool(animBoolName, value);
    }

    public void Target_SetAnimatorFloatParam(string animFloatName, float value)
    {
        _target.Animator.SetFloat(animFloatName, value);
    }

    public void Target_SetVelocityZero()
    {
        _target.Rigidbody.velocity = Vector2.zero;
    }

    public void Target_SetVelocityX(float velocityX)
    {
        _workspaceVector2.Set(velocityX, _target.Rigidbody.velocity.y);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Target_SetVelocityY(float velocityY)
    {
        _workspaceVector2.Set(_target.Rigidbody.velocity.x, velocityY);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Target_SetVelocity(float velocityX, float velocityY)
    {
        _workspaceVector2.Set(velocityX, velocityY);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Target_SetVelocity(float velocity, Vector2 direction)
    {
        _workspaceVector2 = direction * velocity;
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Target_SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Target_SetVelocityXClampVelocityY(float velocityX, float minY, float maxY)
    {
        _workspaceVector2.Set(velocityX, Mathf.Clamp(_target.Rigidbody.velocity.y, minY, maxY));
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Target_SetColliderHight()
    {
        // TODO
    }

    public void Target_SetRigidbodyType(RigidbodyType2D type)
    {
        _target.Rigidbody.bodyType = type;
    }

    public void Target_Flip()
    {
        _target.FacingDirection *= -1;
        _target.transform.Rotate(0f, -180f, 0f);
    }
    #endregion Setters

    #region Checkers
    public bool Target_IsGrounded()
    {
        return Physics2D.OverlapCircle( _target.Sensors.GroundCheck.transform.position,
                                        _target.BaseData.GroundCheckRadius,
                                        _target.BaseData.WhatIsGround);
    }

    public bool Target_IsTouchingCeiling()
    {
        return Physics2D.OverlapCircle( _target.Sensors.LedgeCheck.transform.position,
                                        _target.BaseData.GroundCheckRadius,
                                        _target.BaseData.WhatIsGround);
    }

    public bool Target_IsTouchingWall()
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.transform.right,
                                    _target.BaseData.EnvironmentCheckDistance,
                                    _target.BaseData.WhatIsGround);
    }

    public bool Target_IsTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(   _target.Sensors.LedgeCheck.transform.position,
                                    direction,
                                    _target.BaseData.EnvironmentCheckDistance,
                                    _target.BaseData.WhatIsGround);
    }
    #endregion Checkers
}
