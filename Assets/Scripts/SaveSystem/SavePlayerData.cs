[System.Serializable]
public class SavePlayerData
{
    public float Health;

    public float[] Position;

    public int Level = 1;

    public SavePlayerData(Player player)
    {
        Health = player.CurrentHealth;

        Position = new float[] {
          player.transform.position.x,
          player.transform.position.y,
          player.transform.position.z
        };    
    }
}
