using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Bomb")]
public class BombItem : Item
{
    public GameObject bombPrefab;
    public float bombForce;
    public float bombRadius;
    public int bombDamage;
    GameObject currentBomb;
    public override void Activate(GameObject parent)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        Vector3 throwDirection = (mousePosition - parent.transform.position).normalized;

        currentBomb = Instantiate(bombPrefab, parent.transform.position - new Vector3(0, 0, 1), Quaternion.identity);
        Rigidbody2D bombRb = currentBomb.GetComponent<Rigidbody2D>();

        bombRb.velocity = throwDirection * bombForce;
    }

    public override void BeginCooldown(GameObject parent)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentBomb.transform.position, bombRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Block"))
            {
                CustomEventSystem.current.TextDisplay(collider.transform.position, 3, "+1 " + collider.GetComponent<Tile>().tileDataHolder.tileName, 30, collider.GetComponent<Tile>().tileDataHolder.tileRarity);
                CollectAndDestroyBlock(collider.gameObject);

                // Add Resource to Backpack and Destroy the Block
                parent.GetComponent<Backpack>().AddResource(collider.GetComponent<Tile>().tileDataHolder.id, 1);

                //Track Lifetime Stats
                CustomEventSystem.current.BlockBreak(collider.GetComponent<Tile>().tileDataHolder.tileName, collider.GetComponent<Tile>().tileDataHolder.tileType);
            }

            if (collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(bombDamage);
            }
        }

        Destroy(currentBomb);
    }

    private void CollectAndDestroyBlock(GameObject block)
    {
        // Destroy the block
        Destroy(block);
    }
}
