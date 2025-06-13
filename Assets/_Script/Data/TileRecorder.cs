using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileRecorder : MonoBehaviour
{
    public AudioSource music;
    public List<TileData> recordedTiles;
    private float holdStartTime;
    private bool isHolding;

    void Update()
    {   
        if (isHolding) return;
        if (Input.GetKeyDown(KeyCode.Space)) StartHold();
        if (Input.GetKeyUp(KeyCode.Space)) EndHoldOrTap(TileType.Normal);
        if (Input.GetKeyDown(KeyCode.LeftControl)) StartHold();
        if (Input.GetKeyUp(KeyCode.LeftControl)) EndHoldOrTap(TileType.Double);
    }

    void StartHold()
    {
        isHolding = true;
        holdStartTime = music.time;
    }

    void EndHoldOrTap(TileType type)
    {
        isHolding = false;
        float end = music.time;
        TileData tile = new TileData();
        tile.startTime = holdStartTime;
        tile.endTime = (end - holdStartTime > 0.25f) ? end : 0;
        tile.tileType = (tile.endTime > 0) ? TileType.Long : type;
        recordedTiles.Add(tile);
    }
    public void PlayRecord()
    {
        if (!music.isPlaying)
        {
            recordedTiles = new();
            music.Play();
        }
            
        else 
            music.Stop();
    }

    public void SaveToFile()
    {
        if (recordedTiles.Count == 0) return;
        string json = JsonUtility.ToJson(new TileDataList { tiles = recordedTiles }, true);
        string folderPath = Application.dataPath + "/_MusicSource/";

        string filePath = folderPath + music.clip.name + ".json";
        File.WriteAllText(filePath, json);
    }

    [System.Serializable]
    public class TileDataList
    {
        public List<TileData> tiles;
    }
}
