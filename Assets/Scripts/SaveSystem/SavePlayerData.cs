[System.Serializable]
public class SavePlayerData
{
    public float Health;

    public float[] Position;

    //private int Level;

    public SavePlayerData(Player player)
    {
        Health = player.CurrentHealth;

        Position = new float[] {
          player.transform.position.x,
          player.transform.position.y,
          player.transform.position.z
        };    
    }
    public static GameData Capture()
    {
        return null;
    }

    public static void Apply(GameData data)
    {

    }
}
