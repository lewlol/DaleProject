using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public float miningRange = 2.0f;
    private float miningTime ;
    public float shakeMagnitude = 0.05f; // Adjust this as needed

    private bool isMining = false;
    private float miningTimer = 0.0f;
    private GameObject currentBlock;
    private Vector3 originalBlockPosition;
    private Transform childTransform; // The child transform to shake
    private GameObject lastHighlightedBlock;
    private GameObject previousHighlightedBlock;

    int stamina;
    public PlayerStatsData playerStats;
    public PlayerLifetimeStatsData lifetimeStats;
    public LayerMask ignoreRaycast;
    private int mineAmount;

    //Fortune
    public float fortune;


    private void Start()
    {
        stamina = playerStats.maxstamina;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (lastHighlightedBlock != null && lastHighlightedBlock != hit.collider?.gameObject)
        {
            Unhighlightblock();
        }
        if (lastHighlightedBlock != null && !IsInRange(lastHighlightedBlock))
        {
            Unhighlightblock();
        }
        if (currentBlock != null && (hit.collider == null || hit.collider.gameObject != currentBlock))
        {
            // Reset mining if the cursor moves away from the current block or a new block is highlighted
            isMining = false;
            miningTimer = 0f;
            ResetBlockPosition();
        }

        if (hit.collider != null && hit.collider.CompareTag("Block") && IsInRange(hit.collider.gameObject))
        {
            HightlightBlock(hit.collider.gameObject);
            if (Input.GetMouseButtonDown(0) && CanMine() && CanBreak(hit.collider.gameObject)) // Check if the player can mine
            {
                currentBlock = hit.collider.gameObject;
                childTransform = currentBlock.transform.GetChild(0); // Assuming the child is at index 0
                originalBlockPosition = childTransform.position;
                isMining = true;
                miningTimer = 0.0f;
            }

            if (Input.GetMouseButtonUp(0) && isMining)
            {
                isMining = false;
                miningTimer = 0f;
                ResetBlockPosition();
            }

            if (isMining)
            {
                miningTime = hit.collider.gameObject.GetComponent<Tile>().tileDataHolder.breakTime;
                miningTimer += Time.deltaTime;

                if (miningTimer >= miningTime)
                {
                    BlockBreak(currentBlock);
                    isMining = false;
                    currentBlock = null; // Reset the current block
                }
                else
                {
                    ApplyShake(childTransform);
                }
            }
        }
    }

    private bool IsInRange(GameObject block)
    {
        Vector2 playerPosition = transform.position;
        Vector2 blockPosition = block.transform.position;
        float distance = Vector2.Distance(playerPosition, blockPosition);

        return distance <= miningRange;
    }

    private void BlockBreak(GameObject block)
    {
        MinusStamina(1);

        if (block.GetComponent<Tile>().tileDataHolder.tileType == TileTypes.Rock)
        {
            fortune = playerStats.stonefortune;
            TrackRockMined(block.GetComponent<Tile>().tileDataHolder.tileName);
            
        }
        else if (block.GetComponent<Tile>().tileDataHolder.tileType == TileTypes.Ore)
        {
            fortune = playerStats.orefortune;
            TrackOreMined(block.GetComponent<Tile>().tileDataHolder.tileName);
            
        }
        else if (block.GetComponent<Tile>().tileDataHolder.tileType == TileTypes.Gemstone)
        {
            fortune = playerStats.gemstonefortune;
            TrackGemstoneMined(block.GetComponent<Tile>().tileDataHolder.tileName);
            

        }
        else
        {
            fortune = 1;
        }

        
        int guaranteedAmount = Mathf.FloorToInt(fortune);
        float fractionalPart = fortune - guaranteedAmount;
        int additionalAmount = 0;
        if (Random.value <= fractionalPart)
        {
            additionalAmount = 1;
        }

        mineAmount = guaranteedAmount + additionalAmount;

        // Call 3D Text - Block Transform (Offset Applied in Text Script), Time it stays there, +(Resource Amount) Block Name, Text Size 
        CustomEventSystem.current.TextDisplay(block.gameObject.transform.position, 2f, "+" + mineAmount + " " + block.GetComponent<Tile>().tileDataHolder.tileName, 30, Color.white);

        // Add Resource to Backpack and Destroy the Block
        gameObject.GetComponent<Backpack>().AddResource(block.GetComponent<Tile>().tileDataHolder.id, mineAmount);
        Destroy(block);
    }

    private void ApplyShake(Transform transformToShake)
    {
        if (transformToShake != null)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeMagnitude, shakeMagnitude),
                Random.Range(-shakeMagnitude, shakeMagnitude),
                0
            );

            transformToShake.position = originalBlockPosition + shakeOffset;
        }
    }

    private void ResetBlockPosition()
    {
        if (childTransform != null)
        {
            childTransform.position = originalBlockPosition;
        }
    }

    public void MinusStamina(int amount)
    {
        stamina -= amount;
        if (stamina < 0)
        {
            stamina = 0;
        }
    }

    public void RegenStamina()
    {
        stamina = playerStats.maxstamina;
    }

    private void HightlightBlock(GameObject block)
    {
        if (block != previousHighlightedBlock)
        {
            Unhighlightblock();
            lastHighlightedBlock = block;
            // Get the child object and enable it
            GameObject childObject = block.transform.GetChild(0).gameObject;
            SpriteRenderer childSpriteRenderer = childObject.GetComponent<SpriteRenderer>();
            childSpriteRenderer.enabled = true;
            previousHighlightedBlock = block;
        }
    }

    private void Unhighlightblock()
    {
        if (lastHighlightedBlock != null)
        {
            GameObject childObject = lastHighlightedBlock.transform.GetChild(0).gameObject;
            SpriteRenderer childSpriteRenderer = childObject.GetComponent<SpriteRenderer>();
            childSpriteRenderer.enabled = false;
            lastHighlightedBlock = null;
            previousHighlightedBlock = null;
        }
    }

    private bool CanMine()
    {
        if (stamina >= 1) 
        {
            return true;
        }
        else
        {
           
            Debug.Log("Not enough stamina to mine.");
            return false;
        }
    }
    public bool CanBreak(GameObject block)
    {
        Tile blockTile = block.GetComponent<Tile>();

        if (blockTile != null)
        {
            if(blockTile.tileDataHolder.tileType==TileTypes.Rock)
            {
               
                if (playerStats.stonebreakingpower >= blockTile.tileDataHolder.breakingPower)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Player does not have enough breaking power to break this block.");
                    return false;
                }
            }
            if (blockTile.tileDataHolder.tileType == TileTypes.Ore)
            {
               
                if (playerStats.orebreakingpower >= blockTile.tileDataHolder.breakingPower)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Player does not have enough breaking power to break this block.");
                    return false;
                }
            }
            if (blockTile.tileDataHolder.tileType == TileTypes.Gemstone)
            {
                
                if (playerStats.gemstonebreakingpower >= blockTile.tileDataHolder.breakingPower)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Player does not have enough breaking power to break this block.");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            
            return false;
        }
    }
    private void TrackRockMined(string rockType)
    {
        lifetimeStats.totalrocksmined++;
        switch (rockType)
        {
            case "Stone":
                lifetimeStats.totalstone++;
                break;
            case "Greystone":
                lifetimeStats.totalgreystone++;
                break;
            case "Pinkstone":
                lifetimeStats.totalpinkstone++;
                break;
                // Add cases for other rock types here
        }
    }

    private void TrackOreMined(string oreType)
    {
        lifetimeStats.totaloresmined++; // Increment the total ores mined count

        switch (oreType)
        {
            case "Coal":
                lifetimeStats.totalcoal++;
                break;
            case "Iron":
                lifetimeStats.totaliron++;
                break;
            case "Gold":
                lifetimeStats.totalgold++;
                break;
            case "Copper":
                lifetimeStats.totalcopper++;
                break;
            case "Silver":
                lifetimeStats.totalsilver++;
                break;
            case "Cobalt":
                lifetimeStats.totalcobalt++;
                break;
            case "Lapis Lazuli":
                lifetimeStats.totallapis++;
                break;
            case "Obsidian":
                lifetimeStats.totalobsidian++;
                break;
            case "Mithril":
                lifetimeStats.totalmithril++;
                break;
            case "Adamantite":
                lifetimeStats.totaladmantite++;
                break;
                // Add cases for other ore types here
        }
    }

    private void TrackGemstoneMined(string gemstoneType)
    {
        lifetimeStats.totalgemstonesmined++; // Increment the total gemstones mined count

        switch (gemstoneType)
        {
            case "Ruby":
                lifetimeStats.totalruby++;
                break;
            case "Sapphire":
                lifetimeStats.totalsapphire++;
                break;
            case "Jasper":
                lifetimeStats.totaljasper++;
                break;
            case "Emerald":
                lifetimeStats.totalemerald++;
                break;
            case "Amethyst":
                lifetimeStats.totalamethyst++;
                break;
            case "Moonstone":
                lifetimeStats.totalmoonstone++;
                break;
            case "Diamond":
                lifetimeStats.totaldiamond++;
                break;
            case "Tanzanite":
                lifetimeStats.totaltanzanite++;
                break;
            case "Jadeite":
                lifetimeStats.totaljadeite++;
                break;
            case "Quartz":
                lifetimeStats.totalquartz++;
                break;
            case "Black Onyx":
                lifetimeStats.totalblackonyx++;
                break;
            case "Citrine":
                lifetimeStats.totalcitrine++;
                break;
            case "Rose Quartz":
                lifetimeStats.totalrosequartz++;
                break;
                // Add cases for other gemstone types here
        }
    }


}