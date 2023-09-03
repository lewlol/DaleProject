using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    public List<ArtifactSO> availableArtifacts;
    public List<ArtifactSO> playerArtifacts;

    // Add logic to initialize availableArtifacts with all possible artifacts.

    public void AcquireArtifact(int artifactId)
    {
        // Check if the player already has the artifact
        ArtifactSO playerArtifact = playerArtifacts.Find(artifact => artifact.id == artifactId);

        if (playerArtifact == null)
        {
            // If the player doesn't have the artifact, add the base artifact.
            ArtifactSO baseArtifact = availableArtifacts.Find(artifact => artifact.id == artifactId);
            if (baseArtifact != null)
            {
                playerArtifacts.Add(CloneArtifact(baseArtifact));
            }
        }
        else
        {
            // If the player already has the artifact, upgrade it.
            UpgradeArtifact(playerArtifact);
        }
    }

    private void UpgradeArtifact(ArtifactSO artifact)
    {
       //Happens if you ahve duplicate
        artifact.level++;
        artifact.statBoost += artifact.upgradeIncrement;

    
    }

    private ArtifactSO CloneArtifact(ArtifactSO source)
    {
        // Create a new instance of the artifact with the same data.
        ArtifactSO newArtifact = Instantiate(source);
        newArtifact.hideFlags = HideFlags.None; // Show it in the Unity Editor.

        return newArtifact;
    }
}