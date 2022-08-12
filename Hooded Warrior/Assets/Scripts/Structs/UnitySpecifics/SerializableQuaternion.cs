using UnityEngine;
using System;

[Serializable]
public struct SerializableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SerializableQuaternion(Quaternion values)
    {
        x = values.x;
        y = values.y;
        z = values.z;
        w = values.w;
    }

    public Quaternion GetValues()
    {
        return new Quaternion(x, y, z, w);
    }
}
