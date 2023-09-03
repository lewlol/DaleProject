using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts/Artifact")]
public class ArtifactSO : ScriptableObject
{
    public int id;
    public string artifactname;
    public Sprite sprite;
    public int level;
    public string statUpgrade;
    public float statBoost;
    public float upgradeIncrement;
}