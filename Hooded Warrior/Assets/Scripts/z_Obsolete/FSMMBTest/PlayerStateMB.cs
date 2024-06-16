using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMB : StateMB
{
    private PlayerTest _player;
    
    //[SerializeField] private PlayerControllerParametersEnum animationID;

    protected override void Awake()
    {
        _player = Target as PlayerTest;
    }
}
