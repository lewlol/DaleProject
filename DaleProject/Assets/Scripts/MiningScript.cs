using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public LayerMask ignoreRaycast;
    public float miningrange = 2f;

    private void Update()
    {
        miningblock();
    }

    public void miningblock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, ~ignoreRaycast);

            if (hit.collider != null && hit.collider.CompareTag("Block"))
            {
                if (IsInRange(hit.collider.gameObject))
                {
                    BlockBreak(hit.collider.gameObject);
                }
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