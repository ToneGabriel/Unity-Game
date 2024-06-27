using UnityEngine;


public abstract class EntityMode : FiniteStateMachine
{
    private readonly Entity _target;
    protected Vector2       _workspaceVector2;

    public abstract string[] AnimatorParameterNames { get; }

    public EntityMode(Entity entity)
    {
        _target = entity;
    }

    public void ExitCurrentMode()
    {
        _target.ChangeToNextMainMode();
    }

    #region Setters
    public void Target_SetAnimatorBoolParam(string animBoolName, bool value)
    {
        _target.Animator.SetBool(animBoolName, value);
    }

    public void SetAnimatorFloatParam(string animFloatName, float value)
    {
        _target.Animator.SetFloat(animFloatName, value);
    }

    public void SetVelocityZero()
    {
        _target.Rigidbody.velocity = Vector2.zero;
    }

    public void SetVelocityX(float velocityX)
    {
        _workspaceVector2.Set(velocityX, _target.Rigidbody.velocity.y);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocityY(float velocityY)
    {
        _workspaceVector2.Set(_target.Rigidbody.velocity.x, velocityY);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocityX, float velocityY)
    {
        _workspaceVector2.Set(velocityX, velocityY);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _target.Rigidbody.velocity = velocity;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspaceVector2 = direction * velocity;
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity)     // Set velocity towards facing direction
    {
        _workspaceVector2.Set(_target.FacingDirection * velocity, _target.Rigidbody.velocity.y);
        _target.Rigidbody.velocity = _workspaceVector2;
    }

    public void Flip()
    {
        _target.FacingDirection *= -1;
        _target.transform.Rotate(0f, -180f, 0f);
    }
    #endregion Setters

    #region Checkers
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle( _target.Sensors.GroundCheck.transform.position,
                                        _target.BaseData.GroundCheckRadius,
                                        _target.BaseData.WhatIsGround);
    }

    public bool IsTouchingCeiling()
    {
        return Physics2D.OverlapCircle( _target.Sensors.LedgeCheck.transform.position,
                                        _target.BaseData.GroundCheckRadius,
                                        _target.BaseData.WhatIsGround);
    }

    public bool IsTouchingWall()
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.transform.right,
                                    _target.BaseData.EnvironmentCheckDistance,
                                    _target.BaseData.WhatIsGround);
    }

    public bool IsTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(   _target.Sensors.LedgeCheck.transform.position,
                                    direction,
                                    _target.BaseData.EnvironmentCheckDistance,
                                    _target.BaseData.WhatIsGround);
    }
    #endregion Checkers
}
