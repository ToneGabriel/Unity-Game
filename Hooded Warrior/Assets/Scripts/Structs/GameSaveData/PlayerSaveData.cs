using System;

[Serializable]
public struct PlayerSaveData
{
    public int PlayerFacingDirection;
    public float PlayerHealth;
    public SerializableVector3 PlayerPosition;
    public SerializableQuaternion PlayerRotation;

    public PlayerSaveData(Player player)
    {
        PlayerFacingDirection = player.FacingDirection;
        PlayerHealth = player.CurrentHealth;
        PlayerPosition = new SerializableVector3(player.transform.position);
        PlayerRotation = new SerializableQuaternion(player.transform.rotation);
    }

}
