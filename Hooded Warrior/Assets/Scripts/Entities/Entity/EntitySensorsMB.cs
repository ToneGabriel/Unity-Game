using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntitySensorsMB : MonoBehaviour
{
    public GameObject _groundCheck;
    public GameObject _environmentCheck;
    public GameObject _ledgeCheck;

    private float _groundCheckRadius;
    private float _environmentCheckDistance;
    private float _ledgeCheckRadius;

    private float _targetCloseRangeDistance;
    private float _targetLongRangeDistance;

    private LayerMask _whatIsGround;
    private LayerMask _whatIsTarget;

    public bool Detect_IsTargetCloseRange()
    {
        return Physics2D.Raycast(   _environmentCheck.transform.position,
                                    transform.right,
                                    _targetCloseRangeDistance,
                                    _whatIsTarget);
    }

    public bool Detect_IsTargetLongRange()
    {
        return Physics2D.Raycast(   _environmentCheck.transform.position,
                                    transform.right,
                                    _targetLongRangeDistance,
                                    _whatIsTarget);
    }

    public bool Detect_IsGrounded()
    {
        return Physics2D.OverlapCircle( _groundCheck.transform.position,
                                        _groundCheckRadius,
                                        _whatIsGround);
    }

    public bool Detect_IsTouchingCeiling()
    {
        return Physics2D.OverlapCircle( _ledgeCheck.transform.position,
                                        _groundCheckRadius,
                                        _whatIsGround);
    }

    public bool Detect_IsTouchingWall()
    {
        return Physics2D.Raycast(   _environmentCheck.transform.position,
                                    transform.right,
                                    _environmentCheckDistance,
                                    _whatIsGround);
    }

    public bool Detect_IsTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(   _ledgeCheck.transform.position,
                                    direction,
                                    _environmentCheckDistance,
                                    _whatIsGround);
    }

    public bool Detect_IsCloseToLedge()
    {
        return Physics2D.Raycast(   _ledgeCheck.transform.position,
                                    -transform.up,
                                    _environmentCheckDistance,
                                    _whatIsGround);
    }

    public enum SensorType
    {
        Ground,
        Ceiling,
        Wall,
        Ledge,
        Target
    }
}
