using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    public object CaptureState()
    {
        var state = new Dictionary<string, object>();
        
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
