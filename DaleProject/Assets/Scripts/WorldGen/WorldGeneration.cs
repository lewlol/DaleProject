using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldGeneration : MonoBehaviour
{
    int currentQuadrant;
    Vector2 quadrantOffset;
    Vector3 backgroundPosition;

    bool mineshaftGenerated;
    bool borderGenerated;

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
    public int quadrantAmounts;
    [Range(-10000, 10000)]public int seed;

    [Header("Biomes")]
    public Biome undergroundBiome;
    public Biome[] commonBiomes;
    public Biome[] rareBiomes;
    public Biome[] uniqueBiomes;

    [Header("Tile Prefabs")]
    public GameObject tile;
    public GameObject backgroundTile;
    public GameObject dirtTile;
    public GameObject mineshaftTile;
    public GameObject borderTile;

    [Header("Misc Properties")]
    public GameObject dirt;

    private void Start()
    {
        currentQuadrant = 1;
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

        //Choose Biome
        ChooseBiome();

        //Generate Mineshaft
        GenerateMineshaft(mineshaftGenerated);
        mineshaftGenerated = true;

        //Set Quadrant Offset
        SetQuadrantOffset();

        //Generate Cave Texture - Only Generate Once
        GenerateCaveTexture();

        //Generate Stone + Caves
        GenerateStone();

        //Generate Background
        GenerateBackground();

        //Generate Borders
        GenerateBorders(borderGenerated);
        borderGenerated = true;

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

        //Generate Loot
        GenerateLoot();

        Debug.Log("Quadrant " + currentQuadrant + "is a " + activeBiome.name + " Biome");

        //Check to Generate Another Quadrant
        if(currentQuadrant < quadrantAmounts)
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
                        SetSpawnPoint(x, y);

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
        SetTileName(gem, x + quadrantOffset.x, y + quadrantOffset.y, activeTile.name);
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
                        SetSpawnPoint(x, y);

                        //Delete Rock Block 
                        DeleteBlock(x, y, activeBiome.rockTile.name);

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
                SetTileName(newTile, x, y + quadrantOffset.y, activeTile.name);
                tiles.Add(newTile);

                oreTexture.SetPixel(x, y, Color.black);
                oreTexture.Apply();

                veinCount = 0;
                GenerateOreVein(activeTile.veinChance, activeTile.maxVeinCount, activeTile, x, y);
            }
            if (newOre)
            {
                SetSpawnPoint(x, y);
                GameObject newTile = Instantiate(tile, spawnPosition, Quaternion.identity);
                newTile.GetComponent<Tile>().Ore(activeTile);
                SetTileName(newTile, x, y, activeTile.name);
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
                DeleteBlock(x + 1, y, activeTile.name);
                SpawnOre(x + 1, y, true);
            }
            if (dir == 1)//Left
            {
                DeleteBlock(x - 1, y, activeTile.name);
                SpawnOre(x - 1, y, true);
            }
            if (dir == 2)//Up
            {
                DeleteBlock(x, y + 1, activeTile.name);
                SpawnOre(x, y + 1, true);
            }
            if (dir == 3)//Down
            {
                DeleteBlock(x, y - 1, activeTile.name);
                SpawnOre(x, y - 1, true);
            }
        }
    }
    public void GenerateOreTexture() //To Stop Ores overlapping each other when generating veins
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
        if(currentQuadrant == 1) //Always Stone
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
                    SetSpawnPoint(x, y);

                    SpawnRock(x, y);
                }
            }
        }
    }
    private void SpawnRock(int x, int y)
    {
        GameObject newTile = Instantiate(tile, spawnPosition, Quaternion.identity);
        newTile.GetComponent<Tile>().Rock(activeBiome.rockTile);
        SetTileName(newTile, x + quadrantOffset.x, y + quadrantOffset.y, activeBiome.rockTile.name);
        tiles.Add(newTile);
    }

    public void GenerateCaveTexture()
    {
        caveTexture = new Texture2D(quadrantSize, quadrantSize);

        for (int x = 0; x < caveTexture.width; x++)
        {
            for (int y = 0; y < caveTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * activeBiome.caveSize, (y + seed) * activeBiome.caveSize);

                if (v < activeBiome.caveFrequency)
                {
                    caveTexture.SetPixel(x, y, new Color(0f, 0f, 0f)); // Cave color (black)
                }
                else
                {
                    caveTexture.SetPixel(x, y, new Color(1f, 1f, 1f)); // Solid ground color (white)
                }
            }
        }
        caveTexture.Apply();
    }

    //Deleting Blocks
    private void DeleteBlock(int x, int y, string name)
    {
        string blockName = "X" + x + " " + "Y" + y + " " + "Tile " + name;
        GameObject tile = GameObject.Find(blockName);
        Destroy(tile);
    }

    //Setting Quadrant Position - Limits to 4 Quadrants but for now it works
    private void SetQuadrantOffset()
    {
        quadrantOffset = new Vector2(0, -quadrantSize * currentQuadrant);
    }

    private void GenerateBackground()
    {
        backgroundPosition = new Vector3((quadrantSize / 2) - 0.5f, quadrantOffset.y + (quadrantSize / 2) - 0.5f, 1f);

        GameObject background = Instantiate(backgroundTile, backgroundPosition, Quaternion.identity);
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();

        sr.sprite = activeBiome.biomeBackground;
        sr.size = new Vector2(quadrantSize, quadrantSize);

        background.name = "Quadrant " + currentQuadrant + " Background";
    }

    private void GenerateLoot()
    {
        for (int x = 0; x < quadrantSize; x++)
        {
            for (int y = 0; y < quadrantSize; y++)
            {
                if (caveTexture.GetPixel(x, y).r > 0.5 && oreTexture.GetPixel(x, y) != Color.black)
                {
                    if (caveTexture.GetPixel(x, y - 1).r < 0.5)
                    {
                        //Percentage Chance to Generate Loot
                        float chance = Random.Range(0f, 1f);
                        if (chance <= activeBiome.lootFrequency)
                        {
                            //Generate the Loot
                            SetSpawnPoint(x, y);
                            SpawnLoot(x, y);
                        }
                    }
                }
            }
        }
    }

    private void SpawnLoot(int x, int y)
    {
        GameObject lootTile = Instantiate(tile, spawnPosition, Quaternion.identity);
        lootTile.GetComponent<Tile>().Rock(activeBiome.lootTile);
        SetTileName(lootTile, x + quadrantOffset.x, y + quadrantOffset.y, activeBiome.lootTile.name);
        tiles.Add(lootTile);
    }
    private void GenerateMineshaft(bool generated)
    {
        if (generated) //Quick bool to only generate a mineshaft once
            return;

        //Generate First Row of Blocks
        for(int x = 0; x < quadrantSize; x++)
        {
            SetSpawnPoint(x, 0);
            SpawnRock(x, 0);
        }

        //Generate Dirt Background
        GameObject dirtBackground = Instantiate(dirtTile, new Vector3(49.5f, 1, 9f), Quaternion.identity);
        SpriteRenderer sr = dirtBackground.GetComponent<SpriteRenderer>();
        sr.size = new Vector2(quadrantSize, 3);

        //Generate Mineshaft Walls
        GameObject mineshaftBackground = Instantiate(mineshaftTile, new Vector3(49.5f, 1.25f, 8.9f), Quaternion.identity);
        SpriteRenderer mbsr = mineshaftBackground.GetComponent<SpriteRenderer>();
        mbsr.size = new Vector2(quadrantSize, 1.5f);

        //Generate Lights

        //Reset Stone
        int offset = quadrantSize - 100;
        dirt.transform.position += new Vector3(0, offset, 0);
        Debug.Log("Mineshaft Generated");
    }

    private void GenerateBorders(bool generated)
    {
        if(generated)
            return;

        Vector2 lBorderPos = new Vector2(7, 0);
        GameObject lBorder = Instantiate(borderTile, lBorderPos, Quaternion.identity);
        lBorder.name = "Left Border";

        Vector2 rBorderPos = new Vector2(quadrantSize - 7, 0);
        GameObject rBorder = Instantiate(borderTile, rBorderPos, Quaternion.identity);
        rBorder.name = "Right Border";
    }

    private void SetSpawnPoint(int x, int y)
    {
        spawnPosition = new Vector2(x, y) + quadrantOffset;
    }

    private void SetTileName(GameObject tile, float x, float y, string name)
    {
        tile.name = "X" + x + " " + "Y" + y + " " + "Tile " + name;
    }
}
