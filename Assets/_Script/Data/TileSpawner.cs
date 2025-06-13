using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : BaseSingleton<TileSpawner>
{
    [Header("Pools")]
    public NormalTilePool normalPool;
    public LongTilePool longPool;
    public DoubleTilePool doublePool;

    [Header("Setup")]
    public Transform[] lanes;
    public Transform spawnLine;
    public Transform hitLine;
    public AudioManager music;

    private List<TileData> tiles;
    private float spawnY;
    private float hitY;
    private float fallSpeed;
    private float fallTime = 1f;
    private TileType lastTileType = TileType.Normal;
    private int lastUsedLane = -1;
    private int lastDoublePairType = -1;

    void Start()
    {
        music = AudioManager.Instance;

        spawnY = spawnLine.position.y;
        hitY = hitLine.position.y;

        float distance = Mathf.Abs(spawnY - hitY);
        fallSpeed = distance / fallTime;
    }
    public void ResetSpawner()
    {   
        tiles = new List<TileData>();
        tiles = LevelLoader.Instance.LoadTiles();
    }

    void Update()
    {
        if (tiles == null || music == null || tiles.Count == 0) return;

        float songTime = music.GetSource().time;

        while (tiles.Count > 0)
        {
            float tileSpawnTime = tiles[0].startTime - fallTime;
            if (songTime >= tileSpawnTime)
            {
                SpawnTile(tiles[0]);
                tiles.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
    }

    void SpawnTile(TileData tile)
    {
        if (tile.tileType == TileType.Double)
        {
            SpawnDoubleTile(tile);
            return;
        }

        int lane = GetRandomLane(tile.tileType);
        Transform spawnPoint = lanes[lane];

        TileMover mover = tile.tileType switch
        {
            TileType.Normal => normalPool.GetFromPool(),
            TileType.Long => longPool.GetFromPool(),
            _ => null
        };

        if (mover == null) return;

        mover.transform.position = new Vector3(spawnPoint.position.x, spawnY, spawnLine.position.z);
        mover.Init(fallSpeed, this, tile);
        lastUsedLane = lane;
    }

    void SpawnDoubleTile(TileData tile)
    {
        int laneA = Random.Range(1, 3);
        Transform spawnPoint = lanes[laneA];

        var mover = doublePool.GetFromPool();
        mover.transform.position = new Vector3(spawnPoint.position.x, spawnY, spawnLine.position.z);
        mover.Init(fallSpeed, this, tile);

        lastUsedLane = -1;
    }

    int GetRandomLane(TileType currentType)
    {
        int[] possibleLanes;

        if (currentType == TileType.Double)
        {
            if (lastTileType == TileType.Double)
            {
                lastDoublePairType = 1 - lastDoublePairType;
            }
            else
            {
                if (lastUsedLane == 1 || lastUsedLane == 3)
                    lastDoublePairType = 0;
                else
                    lastDoublePairType = 1;
            }

            int spawnLane = lastDoublePairType == 0 ? 1 : 2;
            lastTileType = currentType;
            return spawnLane;
        }
        else
        {
            if (lastTileType == TileType.Double)
            {
                possibleLanes = lastDoublePairType == 0 ? new int[] { 1, 3 } : new int[] { 0, 2 };
            }
            else
            {
                List<int> lanes = new List<int> {0, 1, 2, 3 };
                if (lastUsedLane != -1)
                    lanes.Remove(lastUsedLane);
                possibleLanes = lanes.ToArray();
            }

            int lane = possibleLanes[Random.Range(0, possibleLanes.Length)];
            lastUsedLane = lane;
            lastTileType = currentType;
            return lane;
        }
    }

    public void RemoveTile() { }
}
