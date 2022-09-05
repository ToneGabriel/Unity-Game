using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    // This script is attached to a gameobject that has at leas 1 ISaveable component


    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        // Loop through all ISaveable components on this gameobject and capture their states into a Dictionary
        foreach (var saveable in GetComponents<ISaveable>())
            state.Add(saveable.GetType().ToString(), saveable.CaptureState());

        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach(var saveable in GetComponents<ISaveable>())
            if (stateDictionary.TryGetValue(saveable.GetType().ToString(), out object value))
                saveable.RestoreState(value);
    }

}
