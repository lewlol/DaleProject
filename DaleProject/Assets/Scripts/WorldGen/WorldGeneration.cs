using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    int currentQuadrant;
    Vector2 quadrantOffset;

    float totalBiomeChance;
    List<Biome> possibleBiomes = new List<Biome>();
    Biome mostCommonBiome;

    Vector2 spawnPosition;

    Biome activeBiome;
    TileData activeTile;

    Texture2D caveTexture;
    Texture2D oreTexture;

    List<GameObject> tiles = new List<GameObject>();

    int veinCount;
    Sprite gemSprite;

    [Header("Terrain Settings")]
    public int quadrantSize;
    [Range(-10000, 10000)]public int seed;

    [Header("Biomes")]
    public Biome undergroundBiome;
    public Biome[] commonBiomes;
    public Biome[] rareBiomes;
    public Biome[] uniqueBiomes;

    [Header("Tile Prefabs")]
    public GameObject tile;

    private void Start()
    {
        GenerateNewWorld();
    }

    //Generate World Void
    public void GenerateNewWorld()
    {
        GenerateQuadrant();
    }

    //Generating a Quadrant
    public void GenerateQuadrant()
    {
        seed = Random.Range(-10000, 10000);

        //Set Quadrant Offset
        SetQuadrantOffset();

        //Choose Biome
        ChooseBiome();

        //Generate Cave Texture - Only Generate Once
        GenerateCaveTexture();

        //Generate Stone + Caves
        GenerateStone();

        //Generate Ore Texture + Veins
        if (activeBiome.hasOre)
        {
            GenerateOreTexture();
            GenerateOre();
        }

        //Generate Crystals
        if (activeBiome.hasGems)
        {
            GenerateGemstones();
        }

        Debug.Log("Quadrant " + currentQuadrant + "is a " + activeBiome.name + " Biome");

        //Check to Generate Another Quadrant
        if(currentQuadrant < 3)
        {
            currentQuadrant++;
            GenerateQuadrant();
        }
    }

    public void GenerateGemstones()
    {
        for (int x = 0; x < quadrantSize; x++)
        {
            for (int y = 0; y < quadrantSize; y++)
            {
                if (caveTexture.GetPixel(x, y).r > 0.5 && oreTexture.GetPixel(x, y) != Color.black)
                {
                    //Do a Spawn Test (Chance to Spawn)
                    float chance = Random.Range(0, 1f);
                    if(chance <= activeBiome.gemFrequency)
                    {
                        spawnPosition = new Vector2(x, y) + quadrantOffset;

                        //Choose Which Gem
                        int ranGem = Random.Range(0, 101);
                        if (ranGem <= 60)
                        {
                            int gem = Random.Range(0, activeBiome.commonGems.Length);
                            activeTile = activeBiome.commonGems[gem];
                        }
                        if (ranGem > 60 && ranGem <= 90)
                        {
                            int gem = Random.Range(0, activeBiome.rareGems.Length);
                            activeTile = activeBiome.rareGems[gem];
                        }
                        if (ranGem > 90 && ranGem <= 100)
                        {
                            int gem = Random.Range(0, activeBiome.uniqueGems.Length);
                            activeTile = activeBiome.uniqueGems[gem];
                        }

                        //DeterminePossibleSpawn
                        if (caveTexture.GetPixel(x + 1, y).r < 0.5)
                        {
                            gemSprite = activeTile.rightCrystal; //Right
                            SpawnGem(x, y);
                        }
                        else if (caveTexture.GetPixel(x - 1, y).r < 0.5)
                        {
                            gemSprite = activeTile.leftCrystal; //Left
                            SpawnGem(x, y);
                        }
                        else if (caveTexture.GetPixel(x, y + 1).r < 0.5)
                        {
                            gemSprite = activeTile.upCrystal; //Normal
                            SpawnGem(x, y);
                        }
                        else if (caveTexture.GetPixel(x, y - 1).r < 0.5)
                        {
                            gemSprite = activeTile.downCrystal;
                            SpawnGem(x, y);
                        }
                    }
                }
            }
        }
    }
    public void SpawnGem(int x, int y)
    {
        GameObject gem = Instantiate(tile, spawnPosition, Quaternion.identity);
        Tile tt = gem.GetComponent<Tile>();
        gem.name = (x + quadrantOffset.x) + "," + (y + quadrantOffset.y);
        tt.Gemstone(activeTile, gemSprite);
        tiles.Add(gem);
    }

    public void GenerateOre()
    {
        for (int x = 0; x < quadrantSize; x++)
        {
            for (int y = 0; y < quadrantSize; y++)
            {
                if (caveTexture.GetPixel(x, y).r < 0.5 && oreTexture.GetPixel(x, y) == Color.white) //Rock Block
                {
                    //Percentage Chance to Generate Ore
                    float chance = Random.Range(0f, 1f);
                    if (chance <= activeBiome.oreFrequency)
                    {
                        //Check Area Level and Change Spawn Position
                        spawnPosition = new Vector2(x, y) + quadrantOffset;

                        //Delete Rock Block
                        DeleteBlock(x, y);

                        //Spawn Ore
                        SpawnOre(x, y, false);
                    }
                }
            }
        }
    }
    public void SpawnOre(int x, int y, bool newOre)
    {
        if (oreTexture.GetPixel(x, y) == Color.white)
        {
            //Choose Ore
            if (!newOre)
            {
                int rarity = Random.Range(0, 101);
                if (rarity <= 60)
                {
                    int ore = Random.Range(0, activeBiome.commonOres.Length);
                    activeTile = activeBiome.commonOres[ore];
                }
                else if(rarity > 60 && rarity <= 90)
                {
                    int ore = Random.Range(0, activeBiome.rareOres.Length);
                    activeTile = activeBiome.rareOres[ore];
                }
                else if (rarity > 90 && rarity <= 100)
                {
                    int ore = Random.Range(0, activeBiome.uniqueOres.Length);
                    activeTile = activeBiome.uniqueOres[ore];
                }

                GameObject newTile = Instantiate(tile, spawnPosition, Quaternion.identity);
                newTile.GetComponent<Tile>().Ore(activeTile);
                newTile.name = (x + quadrantOffset.x) + "," + (y + quadrantOffset.y);
                tiles.Add(newTile);

                oreTexture.SetPixel(x, y, Color.black);
                oreTexture.Apply();

                veinCount = 0;
                GenerateOreVein(activeTile.veinChance, activeTile.maxVeinCount, activeTile, x, y);
            }
            if (newOre)
            {
                spawnPosition = new Vector2(x, y) + quadrantOffset;
                GameObject newTile = Instantiate(tile, spawnPosition, Quaternion.identity);
                newTile.GetComponent<Tile>().Ore(activeTile);
                newTile.name = x + "," + y;
                tiles.Add(newTile);

                oreTexture.SetPixel(x, y, Color.black);
                oreTexture.Apply();

                GenerateOreVein(activeTile.veinChance, activeTile.maxVeinCount, activeTile, x, y);
            }
        }
    }
    public void GenerateOreVein(float veinChance, int maxVein, TileData activeTile, int x, int y)
    {
        //Chance to Spawn Vein
        int vc = Random.Range(0, 101);
        if (vc <= veinChance && veinCount < maxVein)
        {
            int dir = Random.Range(0, 4);
            veinCount++;
            if (dir == 0)//Right
            {
                DeleteBlock(x + 1, y);
                SpawnOre(x + 1, y, true);
            }
            if (dir == 1)//Left
            {
                DeleteBlock(x - 1, y);
                SpawnOre(x - 1, y, true);
            }
            if (dir == 2)//Up
            {
                DeleteBlock(x, y + 1);
                SpawnOre(x, y + 1, true);
            }
            if (dir == 3)//Down
            {
                DeleteBlock(x, y - 1);
                SpawnOre(x, y - 1, true);
            }
        }
    }
    public void GenerateOreTexture()
    {
        oreTexture = new Texture2D(quadrantSize, quadrantSize);

        for (int x = 0; x < oreTexture.width; x++)
        {
            for (int y = 0; y < oreTexture.height; y++)
            {
                oreTexture.SetPixel(x, y, Color.white);
            }
        }
        oreTexture.Apply();
    }

    private void ChooseBiome()
    {
        if(currentQuadrant == 0) //Always Stone
        {
            activeBiome = undergroundBiome;
        }
        else //Try Another Biome
        {
            int randomBiome = Random.Range(0, 101);
            if(randomBiome <= 80) //80% Chance for a Common Biome
            {
                CheckBiomeChance(commonBiomes);
            }
            else if(randomBiome > 80 && randomBiome <= 95) //15% Chance for a Rare Biome
            {
                CheckBiomeChance(rareBiomes);
            }
            else if(randomBiome > 95 && randomBiome <= 100) //5% Chance for a Unique Biome
            {
                CheckBiomeChance(uniqueBiomes);
            }
        }
    }
    private void CheckBiomeChance(Biome[] biomes)
    {
        mostCommonBiome = biomes[0];

        foreach(Biome b in biomes)
        {
            totalBiomeChance += b.biomeFrequency; //Add up Biome Frequency
        }

        float f = Random.Range(0, totalBiomeChance); //If higher than this float, then Add to a list of Possible Biomes
        foreach(Biome b in biomes)
        {
            if(b.biomeFrequency >= f)
            {
                possibleBiomes.Add(b);
            }

            if(b.biomeFrequency < mostCommonBiome.biomeFrequency)
            {
                mostCommonBiome = b;
            }
        }

        if (possibleBiomes.Count == 0)
        {
            activeBiome = mostCommonBiome;
        }
        else
        {
            //Choose Biome Randomly
            int randomBiome = Random.Range(0, possibleBiomes.Count);
            activeBiome = possibleBiomes[randomBiome];
        }

        //Reset Everything
        totalBiomeChance = 0;
        possibleBiomes.Clear();
    }
    private void GenerateStone()
    {
        for (int x = 0; x < quadrantSize; x++)
        {
            for (int y = 0; y < quadrantSize; y++)
            {
                if (caveTexture.GetPixel(x, y).r < 0.5)
                {
                    spawnPosition = new Vector2(x, y) + quadrantOffset;

                    SpawnRock(x, y);
                }
            }
        }
    }
    private void SpawnRock(int x, int y)
    {
        GameObject newTile = Instantiate(tile, spawnPosition, Quaternion.identity);
        newTile.GetComponent<Tile>().Rock(activeBiome.rockTile);
        newTile.name = (x + quadrantOffset.x) + "," + (y + quadrantOffset.y);
        tiles.Add(newTile);
    }

    public void GenerateCaveTexture()
    {
        caveTexture = new Texture2D(quadrantSize, quadrantSize);

        for (int x = 0; x < caveTexture.width; x++)
        {
            for (int y = 0; y < caveTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * activeBiome.caveFrequency, (y + seed) * activeBiome.caveFrequency);
                caveTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        caveTexture.Apply();
    }

    //Deleting Blocks
    private void DeleteBlock(int x, int y)
    {
        string blockName = (x + quadrantOffset.x) + "," + (y + quadrantOffset.y);
        GameObject tile = GameObject.Find(blockName);
        Destroy(tile);
    }

    //Setting Quadrant Position - Limits to 4 Quadrants but for now it works
    private void SetQuadrantOffset()
    {
        if(currentQuadrant == 0)
        {
            quadrantOffset = new Vector2(0, quadrantSize);
        }else if(currentQuadrant == 1)
        {
            quadrantOffset = new Vector2(quadrantSize, quadrantSize);
        }else if(currentQuadrant == 2)
        {
            quadrantOffset = new Vector2(0, 0);
        }else if(currentQuadrant == 3)
        {
            quadrantOffset = new Vector2(quadrantSize, 0);
        }
    }
}
