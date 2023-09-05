using System.Collections;
using System.Collections.Generic;
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
    BiomeData activeBiome; //Active Biome Data
    TileData activeTile; //Current Tile to Place
    List<GameObject> tiles = new List<GameObject>(); //List of Tiles 
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

                    SpawnRock(rockOrePrefab, x, -y);
                }
            }
        }
    }

    public void SpawnRock(GameObject tilePrefab, int x, int y)
    {
        activeTile = areas[areaLevel].rockTile;
        GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        newTile.GetComponent<Tile>().AssignStats(activeTile);
        newTile.name = x + "," + y;
        tiles.Add(newTile);
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
        }
    }

    public void GenerateOre()
    {
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
                if (CaveTexture.GetPixel(x, y).r < 0.5) //Rock Block
                {
                    //Percentage Chance to Generate Ore
                    float chance = Random.Range(0, 1);
                    if(chance <= oreFrequency)
                    {
                        //Check Area Level and Change Spawn Position
                        CheckAreaLevel(-y);
                        spawnPosition = new Vector2(x, -y);

                        //Delete Rock Block
                        DeleteBlock(x, -y);

                        //Spawn Ore
                        SpawnOre(x, -y);
                    }

                    //Run Vein (Check Up, Down, Left, Right for Percentage Chance to Spawn Vein)
                    //Delete Block in that Place if Vein Worked, Then Run it Again in that Position
                }
            }
        }
    }

    public void SpawnOre(int x, int y)
    {
        //Choose Ore
        int rarity = Random.Range(0, 11);
        if(rarity <= 7)
        {
            //Common
            int ore = Random.Range(0, areas[areaLevel].commonOres.Length);
            GameObject newTile = Instantiate(rockOrePrefab, spawnPosition, Quaternion.identity);
            newTile.GetComponent<Tile>().AssignStats(areas[areaLevel].commonOres[ore]);
            newTile.name = x + "," + y;
            tiles.Add(newTile);
        }
        else
        {
            //Rare
            int ore = Random.Range(0, areas[areaLevel].rareOres.Length);
            GameObject newTile = Instantiate(rockOrePrefab, spawnPosition, Quaternion.identity);
            newTile.GetComponent<Tile>().AssignStats(areas[areaLevel].rareOres[ore]);
            newTile.name = x + "," + y;
            tiles.Add(newTile);
        }
    }
    public void DeleteBlock(int x, int y)
    {
        string blockName = x + "," + y;
        GameObject tile = GameObject.Find(blockName);
        Destroy(tile);
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
        //GenerateOre();

        //When Completed Send Event
        CustomEventSystem.current.WorldGenerated();
    }
}
