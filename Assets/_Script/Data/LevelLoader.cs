using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : BaseSingleton<LevelLoader>
{
    public TextAsset jsonFile;

    public List<TileData> LoadTiles()
    {
        TileRecorder.TileDataList list = JsonUtility.FromJson<TileRecorder.TileDataList>(jsonFile.text);
        return list.tiles;
    }
}