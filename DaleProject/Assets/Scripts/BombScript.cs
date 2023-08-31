using UnityEngine;
using System.Collections;
using TMPro;

public class BombScript : MonoBehaviour
{
    public GameObject bombPrefab;
    public float bombForce = 5.0f;
    public float bombRadius = 2.0f;
    public float bombTimer = 2.0f;
    public ParticleSystem particles;

    private bool canThrowBomb = true; // Add this variable
    public float throwCooldown = 3.0f; // Add this variable

    private Vector3 originalCameraPosition;
    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 0.2f;

    private void Start()
    {
        shakeMagnitude = 0.2f;
        shakeDuration = 0.5f;
    }

    private void Update()
    {




        if (Input.GetMouseButtonDown(1) && canThrowBomb ) // Check if the player has bombs and cooldown is over
        {
            ThrowBomb();
        }

    }

    private void ThrowBomb()
    {
        canThrowBomb = false; // Disable bomb throwing

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        Vector3 throwDirection = (mousePosition - transform.position).normalized;

        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();

        bombRb.velocity = throwDirection * bombForce;

        StartCoroutine(ExplodeAfterDelay(bomb));
    }

    private IEnumerator ExplodeAfterDelay(GameObject bomb)
    {
        yield return new WaitForSeconds(bombTimer);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(bomb.transform.position, bombRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Block"))
            {
                CollectAndDestroyBlock(collider.gameObject);
            }
        }
        originalCameraPosition = Camera.main.transform.position;
        StartCoroutine(ScreenShake());

        bomb.GetComponent<CircleCollider2D>().enabled = false;
        bomb.GetComponent<SpriteRenderer>().enabled = false;
        particles = bomb.GetComponentInChildren<ParticleSystem>();
        particles.Play();
        particles.transform.parent = null;
        bomb.GetComponentInChildren<AudioSource>().Play();
        yield return new WaitForSeconds(throwCooldown);// Add this line
        Destroy(bomb);
        Destroy(particles);
        canThrowBomb = true; // Re-enable bomb throwing
    }

    private void CollectAndDestroyBlock(GameObject block)
    {
        // Destroy the block
        Destroy(block);
    }
    private IEnumerator ScreenShake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            Vector3 cameraShake = Random.insideUnitCircle * shakeMagnitude;

            Camera.main.transform.position = originalCameraPosition + new Vector3(cameraShake.x, cameraShake.y, 0.0f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.position = originalCameraPosition;
    }


}