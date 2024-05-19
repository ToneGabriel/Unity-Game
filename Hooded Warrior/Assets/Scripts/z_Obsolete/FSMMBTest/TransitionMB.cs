using System;
using UnityEngine;

public abstract class TransitionMB : MonoBehaviour
{
    public abstract MonoBehaviour TargetObject { get; }
    public abstract int FromStateID { get; }
    public abstract int ToStateID { get; }

    public Func<bool> Condition { get; set; }
}
