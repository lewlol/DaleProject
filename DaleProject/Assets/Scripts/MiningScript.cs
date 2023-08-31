using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public PlayerStatsData playerStats;
    private RaycastHit2D hit;
    public LayerMask ignoreRaycast;

    // Holding variables
    private bool isHolding = false;
    private float currentHoldTime = 0.0f;

    // Shaking Variables
    public float shakeMagnitude = 0.05f; // Adjust this as needed
    private Vector3 originalSpritePosition;
    private Vector3 originalPosition;
    private GameObject tilesprite;

    private Camera cam;

    private int mineAmount;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, ~ignoreRaycast);
        miningblock();
        highlightingblocks();
    }

    public void highlightingblocks()
    {
        if (hit.collider != null && hit.collider.CompareTag("Block"))
        {
            if (IsInRange(hit.collider.gameObject))
            {
                tilesprite = hit.transform.GetChild(0).gameObject;
                if (tilesprite != null && tilesprite.GetComponent<SpriteRenderer>().enabled == false)
                {
                    tilesprite.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if (tilesprite != null && tilesprite.GetComponent<SpriteRenderer>().enabled == true)
                {
                    tilesprite.GetComponent<SpriteRenderer>().enabled = false;
                }

            }
        }
        if (hit.collider == null)
        {
            if (tilesprite != null && tilesprite.GetComponent<SpriteRenderer>().enabled == true)
            {
                tilesprite.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }



    public void miningblock()
    {
       

        if (Input.GetMouseButtonDown(0))
        {

            if (hit.collider != null && hit.collider.CompareTag("Block"))
            {
                if (IsInRange(hit.collider.gameObject))
                {
                    isHolding = true;
                    tilesprite = hit.transform.GetChild(0).gameObject;

                    // Store the original position when interaction starts
                    originalPosition = tilesprite.transform.position;

                    // Store the original local position of the sprite
                    originalSpritePosition = tilesprite.gameObject.GetComponent<SpriteRenderer>().transform.localPosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isHolding)
            {
                currentHoldTime = 0.0f;
                isHolding = false;

                // Reset the sprite's local position
                tilesprite.transform.localPosition = originalSpritePosition;
            }
        }

        if (isHolding)
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= playerStats.miningspeed)
            {
                BlockBreak(hit.collider.gameObject);
                currentHoldTime = 0.0f;
                isHolding = false;
            }

            ApplyShake(tilesprite);
        }
    }

    private void ApplyShake(GameObject block)
    {
        float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
        float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0);

        // Apply the shake offset to the sprite's local position
        tilesprite.GetComponent<SpriteRenderer>().transform.localPosition = originalSpritePosition + shakeOffset;
    }

    private bool IsInRange(GameObject block)
    {
        Vector3 playerPosition = transform.position;
        Vector3 targetPosition = block.transform.position;
        Vector3 direction = targetPosition - playerPosition;
        float distance = Vector3.Distance(playerPosition, targetPosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(playerPosition, direction, distance, ~ignoreRaycast);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Block") && hit.collider.gameObject != block)
            {
                return false;
            }
        }

        return distance <= playerStats.miningrange;
    }

    private void BlockBreak(GameObject block)
    {
        mineAmount = 1;
        gameObject.GetComponent<Backpack>().AddResource(block.GetComponent<Tile>().tileDataHolder.id, mineAmount);
        Destroy(block); // Destroy the block
    }
}