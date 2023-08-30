using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Possible Biomes")]
    public BiomeData[] biome;

    [Header("Terrain Settings")]
    public int worldWidth;
    public int worldHeight;
    [Range(0f, 0.1f)]public float caveFrequency;
    [Range(0f, 0.1f)] public float biomeFrequency;
    [Range(-10000, 10000)]public int seed;

    //Hidden Variables
    Vector2 spawnPosition; //Tile Spawn Pos
    Texture2D CaveTexture; //Cave Noise Texture
    Texture2D biomeTexture; //Biome Noise Texture
    BiomeData activeBiome; //Active Biome Data
    public void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateCaveTexture();
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for(int x = 0; x < worldWidth; x++)
        {
            for(int y = 0; y < worldHeight; y++)
            {
                if(CaveTexture.GetPixel(x, y).r < 0.5)
                {
                    spawnPosition = new Vector2(x, y);
                    SpawnTile();
                }
            }
        }
    }

    public void SpawnTile()
    {
        Instantiate(activeBiome.rockTile, spawnPosition, Quaternion.identity);
    }

    private void GenerateBiomes()
    {

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

    public void GenerateBiomeTexture()
    {
        biomeTexture = new Texture2D(worldWidth * 5, worldHeight * 5);

        for (int x = 0; x < CaveTexture.width; x++)
        {
            for (int y = 0; y < CaveTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * biomeFrequency, (y + seed) * biomeFrequency);
                CaveTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        biomeTexture.Apply();
    }
}
