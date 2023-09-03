using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public float miningRange = 2.0f;
    public float miningTime = 1.0f;

    private bool isMining = false;
    private float miningTimer = 0.0f;
    private GameObject currentBlock;

    int stamina;
    public PlayerStatsData playerStats;
    public LayerMask ignoreRaycast;
    private int mineAmount;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (currentBlock != null && (hit.collider == null || hit.collider.gameObject != currentBlock))
        {
            // Reset mining if the cursor moves away from the current block or a new block is highlighted
            isMining = false;
            currentBlock = null;
            miningTimer = 0f;
        }

        if (hit.collider != null && hit.collider.CompareTag("Block") && IsInRange(hit.collider.gameObject))
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentBlock = hit.collider.gameObject;
                isMining = true;
                miningTimer = 0.0f;
            }

            if (Input.GetMouseButtonUp(0) && isMining)
            {
                isMining = false;
                miningTimer = 0f;
            }

            if (isMining)
            {
                miningTimer += Time.deltaTime;

                if (miningTimer >= miningTime)
                {
                    BlockBreak(currentBlock);
                    isMining = false;
                    currentBlock = null; // Reset the current block
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

        mineAmount = 1;

        // Call 3D Text - Block Transform (Offset Applied in Text Script), Time it stays there, +(Resource Amount) Block Name, Text Size 
        CustomEventSystem.current.TextDisplay(block.gameObject.transform.position, 2f, "+" + mineAmount + " " + block.GetComponent<Tile>().tileDataHolder.tileName, 30, Color.white);

        // Add Resource to Backpack and Destroy the Block
        gameObject.GetComponent<Backpack>().AddResource(block.GetComponent<Tile>().tileDataHolder.id, mineAmount);
        Destroy(block);
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
}