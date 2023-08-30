using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public LayerMask ignoreRaycast;
    public float miningrange = 2f;

    //Holding variables
    private bool isHolding = false;
    private float currentHoldTime = 0.0f;
    public float requiredHoldTime = 2.0f; 



    private Camera cam;

    private void Start()
    {
        cam= Camera.main;
    }
    private void Update()
    {
        miningblock();
    }

    public void miningblock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, ~ignoreRaycast);

            if (hit.collider != null && hit.collider.CompareTag("Block"))
            {
                if (IsInRange(hit.collider.gameObject))
                {
                    isHolding = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isHolding)
            {
                currentHoldTime = 0.0f;
                isHolding = false;
            }
        }

        if (isHolding)
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= requiredHoldTime)
            {
                BlockBreak(hit.collider.gameObject);
                currentHoldTime = 0.0f;
                isHolding = false;
            }
        }
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

        return distance <= miningrange;
    }

    private void BlockBreak(GameObject block)
    {
        Destroy(block); // Destroy the block
    }
}