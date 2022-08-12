using UnityEngine;
using System;

[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 values)
    {
        x = values.x;
        y = values.y;
        z = values.z;
    }

    public Vector3 GetValues()
    {
        return new Vector3(x, y, z);
    }


}
