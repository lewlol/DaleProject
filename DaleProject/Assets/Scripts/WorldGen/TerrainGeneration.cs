using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Areas")]
    public WorldAreaData[] areas;
    int areaLevel;

    [Header("Terrain Settings")]
    public int worldWidth;
    public int worldHeight;
    [Range(0f, 0.1f)]public float caveFrequency;
    [Range(0f, 1f)]public float oreFrequency;
    [Range(0f, 0.1f)] public float biomeFrequency;
    [Range(-10000, 10000)]public int seed;

    [Header("Blank Tiles")]
    public GameObject rockOrePrefab;

    //Hidden Variables
    Vector2 spawnPosition; //Tile Spawn Pos
    Texture2D CaveTexture; //Cave Noise Texture
    Texture2D OreTexture;
    BiomeData activeBiome; //Active Biome Data
    TileData activeTile; //Current Tile to Place
    public void Start()
    {
        GenerateNewWorld();
    }

    private void GenerateStone()
    {
        for(int y = 0; y < worldHeight; y++)
        {
            for(int x = 0; x < worldWidth; x++)
            {
                if (CaveTexture.GetPixel(x, y).r < 0.5)
                {
                    CheckAreaLevel(-y);
                    spawnPosition = new Vector2(x, -y);

                    SpawnTile(rockOrePrefab);
                }
            }
        }
    }

    public void SpawnTile(GameObject tilePrefab)
    {
        GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        newTile.GetComponent<Tile>().AssignStats(activeTile);
    }

    public void GenerateCaveTexture()
    {
        CaveTexture = new Texture2D(worldWidth * 2, worldHeight * 2);

        for (int x = 0; x < CaveTexture.width; x++)
        {
            for (int y = 0; y < CaveTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFrequency, (y + seed) * caveFrequency);
                CaveTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        CaveTexture.Apply();
    }

    public void CheckAreaLevel(int yLevel)
    {
        if(yLevel < areas[areaLevel].bottomLayerY)
        {
            areaLevel++;
            Debug.Log("Change Area");
        }
    }

    public void GenerateOre()
    {
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                if (OreTexture.GetPixel(x, y).r < 0.5)
                {
                    CheckAreaLevel(-y);
                    spawnPosition = new Vector2(x, -y);

                    SpawnTile(rockOrePrefab);
                }
            }
        }
    }

    public void GenerateOreTexture()
    {
        OreTexture = new Texture2D(worldWidth, worldHeight);
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                float v = Mathf.PerlinNoise((x + seed) * oreFrequency, (y + seed) * oreFrequency);
                OreTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        OreTexture.Apply();
    }

    public void DeleteBlock(int x, int y)
    {

    }
    private void GenerateNewWorld()
    {
        //Reset Certain Variables
        areaLevel = 0;

        //Random Seed
        seed = Random.Range(-10000, 10000);

        //Voids
        GenerateCaveTexture();
        GenerateOreTexture();
        GenerateStone();
        GenerateOre();

        //When Completed Send Event
        CustomEventSystem.current.WorldGenerated();
    }
}
