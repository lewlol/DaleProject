using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MiningLight : MonoBehaviour
{
    public GameObject PlayerLight; 
    public float ylevel = 1.9f;
    public PlayerStatsData playerstats;
    public float helmetlight;

    private void Update()
    {
        //How bright his helmet is 
        helmetlight = 4 + playerstats.helmetlight;
        PlayerLight.GetComponent<Light2D>().pointLightOuterRadius = helmetlight;

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