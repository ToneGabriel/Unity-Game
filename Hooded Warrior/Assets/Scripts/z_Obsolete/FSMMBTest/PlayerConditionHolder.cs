using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionHolder : ConditionHolder
{
    private PlayerTest _player;

    private void Awake()
    {
        _player = Target as PlayerTest;
    }

    [Condition]
    public bool IdleToMove() => InputManager.Instance.NormalizedInputX != 0;

    [Condition]
    public bool IdleToCrouchIdle() => InputManager.Instance.NormalizedInputY == -1;

    //[Condition]
    //public bool IdleToJump() => InputManager.Instance.JumpInput && _player.CanJump();
}
