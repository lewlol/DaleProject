using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public GameObject textmesh;

    private void Start()
    {
        CustomEventSystem.current.onTextDisplay += GenerateText;
    }
    public void GenerateText(Vector3 position, float time, string text, int size, Color color)
    { 
        Vector2 Offset = new Vector2(Random.Range(-0.5f , 0.5f),Random.Range(-0.5f, 0.5f));
        GameObject textMesh = Instantiate(textmesh, new Vector3(position.x + Offset.x, position.y + Offset.y, -2.5f), Quaternion.identity);
        TextMesh tmp = textMesh.GetComponent<TextMesh>();

        tmp.color = color;
        tmp.text = text;
        tmp.fontSize = size; //30 is normally a good size
        Destroy(textMesh, time);
    }
}
