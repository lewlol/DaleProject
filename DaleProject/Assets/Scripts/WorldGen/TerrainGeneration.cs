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
    [Range(0f, 0.1f)] public float biomeFrequency;
    [Range(-10000, 10000)]public int seed;

    [Header("Blank Tiles")]
    public GameObject RockTile;

    //Hidden Variables
    Vector2 spawnPosition; //Tile Spawn Pos
    Texture2D CaveTexture; //Cave Noise Texture
    BiomeData activeBiome; //Active Biome Data
    public void Start()
    {
        GenerateNewWorld();
    }

    private void GenerateStone()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                if (CaveTexture.GetPixel(x, y).r < 0.5)
                {
                    CheckAreaLevel(y);
                    spawnPosition = new Vector2(x, y);
                    SpawnTile(RockTile);
                }
            }
        }
    }

    public void SpawnTile(GameObject tilePrefab)
    {
        GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        newTile.GetComponent<Tile>().AssignStats(areas[areaLevel].rockTile);
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
        if(yLevel < areas[areaLevel].topLayerY)
        {
            areaLevel++;
        }
    }

    private void GenerateNewWorld()
    {
        //Reset Certain Variables
        areaLevel = 0;

        //Random Seed
        seed = Random.Range(-10000, 10000);

        //Voids
        GenerateCaveTexture();
        GenerateStone();
    }
}
