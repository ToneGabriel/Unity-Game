using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class Data_Dead : ScriptableObject
{
    public GameObject DeathChunkParticle;
    public GameObject DeathBloodParticle;
}
