

public class EnemyData
{
    float Health;

    float[] pos;

    public EnemyData(float[] pos, float health)
    {
        this.pos = pos;
        this.Health = health;
    }
}
[System.Serializable]


public class GameData
{

    float SoundVolume;

    int LastLevel;

    int MeleeEnemyCount;

    int RangedEnemyCount;
    

    
    public GameData()
    {

    }


}