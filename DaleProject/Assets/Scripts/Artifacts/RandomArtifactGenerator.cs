using UnityEngine;

public class RandomArtifactGenerator : MonoBehaviour
{
    public ArtifactManager artifactManager;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            int randomArtifactID = Random.Range(0, 3); // Generates 0, 1, or 2
            artifactManager.AcquireArtifact(randomArtifactID);
        }
    }
}