using UnityEngine;

public class PlayerTransitionMB : TransitionMB
{
    [SerializeField] protected Player           _player;
    [SerializeField] protected PlayerStateID    _fromState;
    [SerializeField] protected PlayerStateID    _toState;

    public override MonoBehaviour               TargetObject { get { return _player; } }
    public override int                         FromStateID { get { return (int)_fromState; } }
    public override int                         ToStateID { get { return (int)_toState; } }
}
