using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public GameObject textmesh;
    Color blockTextColor;

    private void Start()
    {
        CustomEventSystem.current.onTextDisplay += GenerateText;
    }
    public void GenerateText(Vector3 position, float time, string text, int size, Rarity tileRarity)
    {
        RarityColor(tileRarity); //Set the Color based on the Rarity

        Vector2 Offset = new Vector2(Random.Range(-0.5f , 0.5f),Random.Range(-0.5f, 0.5f));
        GameObject textMesh = Instantiate(textmesh, new Vector3(position.x + Offset.x, position.y + Offset.y, -9), Quaternion.identity);
        TextMesh tmp = textMesh.GetComponent<TextMesh>();

        tmp.color = blockTextColor;
        tmp.text = text;
        tmp.fontSize = size; //30 is normally a good size
        Destroy(textMesh, time);
    }

    public void RarityColor(Rarity tileRarity)
    {
        switch (tileRarity)
        {
            case Rarity.Common:
                blockTextColor = Color.white;
                break;

            case Rarity.Uncommon:
                blockTextColor = Color.green;
                break;

            case Rarity.Rare:
                blockTextColor = Color.blue;
                break;

            case Rarity.Unique:
                blockTextColor = Color.magenta;
                break;

            case Rarity.Legendary:
                blockTextColor = Color.yellow;
                break;

            case Rarity.Mythic:
                blockTextColor = Color.red;
                break;
        }
    }
}
