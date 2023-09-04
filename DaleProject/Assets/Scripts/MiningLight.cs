using UnityEngine;

public class MiningLight : MonoBehaviour
{
    public GameObject PlayerLight; 
    public float ylevel = 1.9f; 

    private void Update()
    {
        // Assuming the player's transform is used to determine the Y position
        float playerY = transform.position.y;

        if (playerY < ylevel)
        {
           
            PlayerLight.SetActive(true);
        }
        else
        {
           
            PlayerLight.SetActive(false);
        }
    }
}