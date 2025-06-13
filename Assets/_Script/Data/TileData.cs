public enum TileType
{
    Normal,
    Long,
    Double
}

[System.Serializable]
public class TileData
{
    public float startTime;
    public float endTime;
    public TileType tileType;
}
