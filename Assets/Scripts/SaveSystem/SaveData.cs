[System.Serializable]
public class SaveData
{
    public float Health;

    public float[] Position;

    //private int Level;

    public SaveData(Player player)
    {
        Health = player.CurrentHealth;

        Position = new float[] {
          player.transform.position.x,
          player.transform.position.y,
          player.transform.position.z
        };    
    }
}