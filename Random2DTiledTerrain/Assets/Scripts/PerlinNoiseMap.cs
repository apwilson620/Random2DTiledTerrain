using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;
    public GameObject prefab_ocean;
    public GameObject prefab_grass;
    public GameObject prefab_forest;
    public GameObject prefab_swamp;
    public GameObject prefab_hill;
    public GameObject prefab_mountain;
    public GameObject prefab_cave;

    int map_width = 100;
    int map_height = 100;

    public GameObject player;

    int chunkBorder = 32;

    //List<List<int>> noise_grid = new List<List<int>>();
    //List<List<GameObject>> tile_grid = new List<List<GameObject>>();

    float magnification = 7.0f;

    //int currentChunkX = (int)player.transform.position.x / 16;
    //int currentChunkY = (int)player.transform.position.y / 16;

    int x_offset = 0;//UnityEngine.Random.Range(-10000, 10000);
    int y_offset = 0;//UnityEngine.Random.Range(-10000, 10000);

    bool hasStarted = false;
    int currentChunkX = 0;
    int currentChunkY = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(1 / 16);
        //int x_offset = UnityEngine.Random.Range(-10000, 10000);
        //int y_offset = UnityEngine.Random.Range(-10000, 10000);
        //CreateTileset();
        //CreateTileGroups();
        //GenerateMap(x_offset, y_offset);
    }

    // Update is called once per frame
    void Update()
    {
        float playerXPos = player.transform.localPosition.x;
        float playerYPos = player.transform.localPosition.y;
        //Debug.Log(playerXPos + "player pos");
        //Debug.Log(currentChunkX + "chunk");
        if (hasStarted == false)
        {
            //Debug.Log("test1");
            x_offset = UnityEngine.Random.Range(-10000, 10000);
            y_offset = UnityEngine.Random.Range(-10000, 10000);
            CreateTileset();
            CreateTileGroups();
            GenerateMap(x_offset, y_offset, currentChunkX, currentChunkY);
            hasStarted = true;
            //playerXPos = (int)player.transform.position.x;
            //playerYPos = (int)player.transform.position.y;
            currentChunkX = (int)(playerXPos / chunkBorder);
            currentChunkY = (int)(playerYPos / chunkBorder);
        }
        checkPos(x_offset, y_offset, currentChunkX, currentChunkY, playerXPos, playerYPos);
        //if ((playerXPos / 16) != currentChunkX || (playerYPos / 16) != currentChunkY)
        //{
        //Debug.Log("test2");
        //playerXPos = (int)player.transform.position.x;
        //playerYPos = (int)player.transform.position.y;
        //currentChunkX = playerXPos / 16;
        //currentChunkY = playerYPos / 16;
        //GenerateMap(x_offset, y_offset, currentChunkX, currentChunkY);
        //deleteChunks();
        //}
    }

    void CreateTileset()
    {
        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, prefab_ocean);
        tileset.Add(1, prefab_grass);
        tileset.Add(2, prefab_forest);
        tileset.Add(3, prefab_swamp);
        tileset.Add(4, prefab_hill);
        tileset.Add(5, prefab_mountain);
        tileset.Add(6, prefab_cave);
    }

    void CreateTileGroups()
    {
        tile_groups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in tileset)
        {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0, 0, 0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    void checkPos(int x_offset, int y_offset, int currentChunkX, int currentChunkY, float playerXPos, float playerYPos)
    {
        if ((playerXPos / chunkBorder) != currentChunkX || (playerYPos / chunkBorder) != currentChunkY)
        {
            //Debug.Log(currentChunkX);
            //Debug.Log(currentChunkY);
            playerXPos = player.transform.position.x;
            playerYPos = player.transform.position.y;
            currentChunkX = (int)(playerXPos / chunkBorder);
            currentChunkY = (int)(playerYPos / chunkBorder);
            GenerateMap(x_offset, y_offset, currentChunkX, currentChunkY);
            deleteChunks();
        }
        //if ((playerXPos / 16) > currentChunkX || (playerYPos / 16) < currentChunkY)
        //{
        //    Debug.Log("testSE");
        //    playerXPos = player.transform.position.x;
        //    playerYPos = player.transform.position.y;
        //    currentChunkX = (int)(playerXPos / 16);
        //    currentChunkY = (int)(playerYPos / 16);
        //    UpdateChunkSE(x_offset, y_offset, currentChunkX, currentChunkY);
        //    deleteChunks();
        //}
        //if ((playerXPos / 16) < currentChunkX || (playerYPos / 16) < currentChunkY)
        //{
        //    Debug.Log("testSW");
        //    playerXPos = player.transform.position.x;
        //    playerYPos = player.transform.position.y;
        //    currentChunkX = (int)(playerXPos / 16);
        //    currentChunkY = (int)(playerYPos / 16);
        //    UpdateChunkSW(x_offset, y_offset, currentChunkX, currentChunkY);
        //    deleteChunks();
        //}
        //if ((playerXPos / 16) < currentChunkX || (playerYPos / 16) > currentChunkY)
        //{
        //    Debug.Log("testNW");
        //    playerXPos = player.transform.position.x;
        //    playerYPos = player.transform.position.y;
        //    currentChunkX = (int)(playerXPos / 16);
        //    currentChunkY = (int)(playerYPos / 16);
        //    UpdateChunkNW(x_offset, y_offset, currentChunkX, currentChunkY);
        //    deleteChunks();
        //}
    }

    //void UpdateChunkNE(int x_offset, int y_offset, int currentChunkX, int currentChunkY)
    //{
    //    for (int x = 0 + (currentChunkX * chunkBorder); x < map_width + (currentChunkX * chunkBorder); x++)
    //    {
    //        //noise_grid.Add(new List<int>());
    //        //tile_grid.Add(new List<GameObject>());
    //        for (int y = 0 + (currentChunkY * chunkBorder); y < map_height + (currentChunkY * chunkBorder); y++)
    //        {
    //            int tile_id = GetIdUsingPerlin(x, y, x_offset, y_offset);
    //            //noise_grid[x].Add(tile_id);
    //            CreateTile(tile_id, x, y, x_offset, y_offset);
    //        }
    //    }
    //}

    //void UpdateChunkSE(int x_offset, int y_offset, int currentChunkX, int currentChunkY)
    //{
    //    for (int x = 0 + (currentChunkX * chunkBorder); x < map_width + (currentChunkX * chunkBorder); x++)
    //    {
    //        //noise_grid.Add(new List<int>());
    //        //tile_grid.Add(new List<GameObject>());
    //        for (int y = (currentChunkY * chunkBorder) - 1; y > (currentChunkY * chunkBorder) - map_height - 1; y--)
    //        {
    //            int tile_id = GetIdUsingPerlin(x, y, x_offset, y_offset);
    //            //noise_grid[x].Add(tile_id);
    //            CreateTile(tile_id, x, y, x_offset, y_offset);
    //        }
    //    }
    //}

    //void UpdateChunkSW(int x_offset, int y_offset, int currentChunkX, int currentChunkY)
    //{
    //    for (int x = (currentChunkX * chunkBorder) - map_width - 1; x < (currentChunkX * chunkBorder); x++)
    //    {
    //        //noise_grid.Add(new List<int>());
    //        //tile_grid.Add(new List<GameObject>());
    //        for (int y = (currentChunkY * chunkBorder) - 1; y > (currentChunkY * chunkBorder) - map_height - 1; y--)
    //        {
    //            int tile_id = GetIdUsingPerlin(x, y, x_offset, y_offset);
    //            //noise_grid[x].Add(tile_id);
    //            CreateTile(tile_id, x, y, x_offset, y_offset);
    //        }
    //    }
    //}

    //void UpdateChunkNW(int x_offset, int y_offset, int currentChunkX, int currentChunkY)
    //{
    //    for (int x = (currentChunkX * chunkBorder) - map_width - 1; x < (currentChunkX * chunkBorder); x++)
    //    {
    //        //noise_grid.Add(new List<int>());
    //        //tile_grid.Add(new List<GameObject>());
    //        for (int y = 0 + (currentChunkY * chunkBorder); y < map_height + (currentChunkY * chunkBorder); y++)
    //        {
    //            int tile_id = GetIdUsingPerlin(x, y, x_offset, y_offset);
    //            //noise_grid[x].Add(tile_id);
    //            CreateTile(tile_id, x, y, x_offset, y_offset);
    //        }
    //    }
    //}

    void GenerateMap(int x_offset, int y_offset, int currentChunkX, int currentChunkY)
    {
        var startX = (currentChunkX * chunkBorder) - (map_width / 2);
        var xLimit = (map_width / 2) + (currentChunkX * chunkBorder);
        var startY = (currentChunkY * chunkBorder) - (map_height / 2);
        var yLimit = (map_height / 2) + (currentChunkY * chunkBorder);

        for (int x = startX; x < xLimit; x++)
        {
            //noise_grid.Add(new List<int>());
            //tile_grid.Add(new List<GameObject>());
            for (int y = startY; y < yLimit; y++)
            {
                int tile_id = GetIdUsingPerlin(x, y, x_offset, y_offset);
                //noise_grid[x].Add(tile_id);
                CreateTile(tile_id, x, y, x_offset, y_offset);
            }
        }
        //UpdateChunkNE(x_offset, y_offset, currentChunkX, currentChunkY);
        //UpdateChunkSE(x_offset, y_offset, currentChunkX, currentChunkY);
        //UpdateChunkSW(x_offset, y_offset, currentChunkX, currentChunkY);
        //UpdateChunkNW(x_offset, y_offset, currentChunkX, currentChunkY);
    }

    int GetIdUsingPerlin(int x, int y, int x_offset, int y_offset)
    {
        float raw_perlin = Mathf.PerlinNoise((x - x_offset) / magnification, (y - y_offset) / magnification);
        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scale_perlin = clamp_perlin * tileset.Count;
        if (scale_perlin == 7)
        {
            scale_perlin = 6;
        }
        return Mathf.FloorToInt(scale_perlin);
    }

    void CreateTile(int tile_id, int x, int y, int x_offset, int y_offset)
    {
        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);

        //tile_grid[x].Add(tile);
    }

    void deleteChunks()
    {

    }
}
