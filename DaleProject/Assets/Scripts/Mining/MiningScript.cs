using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
    public LayerMask ignoreRaycast;
    private int mineAmount;

    //Fortune
    public float fortune;

    private Color blockTextColor;

    private void Start()
    {
        stamina = playerStats.maxstamina;

        StartCoroutine(SetStamUI());

        CustomEventSystem.current.onSleep += RegenStamina;
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
        }
        else if (block.GetComponent<Tile>().tileDataHolder.tileType == TileTypes.Ore)
        {
            fortune = playerStats.orefortune;
        }
        else if (block.GetComponent<Tile>().tileDataHolder.tileType == TileTypes.Gemstone)
        {
            fortune = playerStats.gemstonefortune;         
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
        if(block.GetComponent<Tile>().tileDataHolder.tileType != TileTypes.Loot)
        {
            CustomEventSystem.current.TextDisplay(block.gameObject.transform.position, 2f, "+" + mineAmount + " " + block.GetComponent<Tile>().tileDataHolder.tileName, 35, block.GetComponent<Tile>().tileDataHolder.tileRarity);
        }

        // Add Resource to Backpack and Destroy the Block
        if (block.GetComponent<Tile>().tileDataHolder.isInventory)
        {
            gameObject.GetComponent<Backpack>().AddResource(block.GetComponent<Tile>().tileDataHolder, mineAmount);
        }

        //Track Lifetime Stats
        CustomEventSystem.current.BlockBreak(block.GetComponent<Tile>().tileDataHolder.tileName, block.GetComponent<Tile>().tileDataHolder.tileType);

        //Loot Check
        if(block.GetComponent<Tile>().tileDataHolder.tileType == TileTypes.Loot)
        {
            block.GetComponent<LootScript>().AssignLootTable(block.GetComponent<Tile>().tileDataHolder.lootTable);
            block.GetComponent<LootScript>().RunLoot();
        }
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

        CustomEventSystem.current.StaminaChange(stamina, playerStats.maxstamina);
    }

    public void AddStamina(int amount)
    {
        stamina += amount;
        if (stamina < playerStats.maxstamina)
        {
            RegenStamina();
        }

        CustomEventSystem.current.StaminaChange(stamina, playerStats.maxstamina);
    }

    public void RegenStamina()
    {
        stamina = playerStats.maxstamina;
        CustomEventSystem.current.StaminaChange(stamina, playerStats.maxstamina);
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
            CustomEventSystem.current.IndicatorMessage("You Are Out of Stamina", 3, false);
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
                    CustomEventSystem.current.IndicatorMessage("You Cannot Break This Yet", 3, false);
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
                    CustomEventSystem.current.IndicatorMessage("You Cannot Break This Yet", 3, false);
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
                    CustomEventSystem.current.IndicatorMessage("You Cannot Break This Yet", 3 , false);
                    return false;
                }
            }
            if (blockTile.tileDataHolder.tileType == TileTypes.Loot)
            {
                return true;
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

    IEnumerator SetStamUI()
    {
        yield return new WaitForSeconds(0.5f);
        CustomEventSystem.current.StaminaChange(stamina, playerStats.maxstamina);
    }
}