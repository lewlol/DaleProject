using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject tile;
    Vector2 spawnPosition;

    [Header("Terrain Settings")]
    public int worldWidth;
    public int worldHeight;
    [Range(0f, 0.1f)]public float caveFrequency;
    [Range(-10000, 10000)]public int seed;
    Texture2D noiseTexture;

    public void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for(int x = 0; x < worldWidth; x++)
        {
            for(int y = 0; y < worldHeight; y++)
            {
                if(noiseTexture.GetPixel(x, y).r < 0.5)
                {
                    spawnPosition = new Vector2(x, y);
                    SpawnTile();
                }
            }
        }
    }

    public void SpawnTile()
    {
        Instantiate(tile, spawnPosition, Quaternion.identity);
    }

    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldWidth * 2, worldHeight * 2);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFrequency, (y + seed) * caveFrequency);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        noiseTexture.Apply();
    }
}
