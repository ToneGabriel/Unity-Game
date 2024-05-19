using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : StateBehaviour
{
    [Condition]
    private bool IdleToMove() => false;

    [Condition]
    private bool IdleToCrouchIdle() => false;
}
