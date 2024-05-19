using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionHolder : MonoBehaviour
{
    [SerializeField]
    [ReadOnlyField]
    public StateBehaviour Target;   // FSMMB will set it
}
