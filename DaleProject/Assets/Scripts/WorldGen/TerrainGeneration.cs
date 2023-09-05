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
    int veinCount; //Vein Counter
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
                    float chance = Random.Range(0f, 1f);
                    if(chance <= oreFrequency)
                    {
                        //Check Area Level and Change Spawn Position
                        CheckAreaLevel(-y);
                        spawnPosition = new Vector2(x, -y);

                        //Delete Rock Block
                        DeleteBlock(x, -y);

                        //Spawn Ore
                        SpawnOre(x, -y, false);
                    }

                    //Run Vein (Check Up, Down, Left, Right for Percentage Chance to Spawn Vein)
                    //Delete Block in that Place if Vein Worked, Then Run it Again in that Position
                }
            }
        }
    }

    public void SpawnOre(int x, int y, bool newOre)
    {
        //Choose Ore
        if (!newOre)
        {
            int rarity = Random.Range(0, 11);
            if (rarity <= 7)
            {
                //Common
                int ore = Random.Range(0, areas[areaLevel].commonOres.Length);
                activeTile = areas[areaLevel].commonOres[ore];
            }
            else
            {
                //Rare
                int ore = Random.Range(0, areas[areaLevel].rareOres.Length);
                activeTile = areas[areaLevel].rareOres[ore];
            }

            GameObject newTile = Instantiate(rockOrePrefab, spawnPosition, Quaternion.identity);
            newTile.GetComponent<Tile>().AssignStats(activeTile);
            newTile.name = x + "," + y;
            tiles.Add(newTile);

            veinCount = 0;
            GenerateOreVein(activeTile.veinChance, activeTile.maxVeinCount, activeTile, x, y);
        }
        if(newOre)
        {
            spawnPosition = new Vector2(x, y);
            GameObject newTile = Instantiate(rockOrePrefab, spawnPosition, Quaternion.identity);
            newTile.GetComponent<Tile>().AssignStats(activeTile);
            newTile.name = x + "," + y;
            tiles.Add(newTile);

            GenerateOreVein(activeTile.veinChance, activeTile.maxVeinCount, activeTile, x, y);
        }

    }

    public void GenerateOreVein(float veinChance, int maxVein, TileData activeTile, int x, int y)
    {
        //Chance to Spawn Vein
        int vc = Random.Range(0, 101);
        if(vc <= veinChance && veinCount < maxVein)
        {
            int dir = Random.Range(0, 4);
            veinCount++;
            if(dir == 0)//Right
            {
                DeleteBlock(x + 1, y);
                SpawnOre(x + 1, y, true);
            }
            if(dir == 1)//Left
            {
                DeleteBlock(x - 1, y);
                SpawnOre(x - 1, y, true);
            }
            if(dir == 2)//Up
            {
                DeleteBlock(x, y + 1);
                SpawnOre(x, y + 1, true);
            }
            if(dir == 3)//Down
            {
                DeleteBlock(x, y - 1);
                SpawnOre(x, y - 1, true);
            }
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
        GenerateOre();

        //When Completed Send Event
        CustomEventSystem.current.WorldGenerated();
    }
}
