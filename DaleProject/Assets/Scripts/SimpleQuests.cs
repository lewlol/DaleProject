using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQuests : MonoBehaviour
{
    int stonequest;
    int coalQuest;
    private void Start()
    {
        CustomEventSystem.current.onBlockBreak += Break50Stone;
    }
    public void Break50Stone(string blockName, TileTypes blockType)
    {
        if(blockName == "Stone")
        {
            stonequest++;
            if(stonequest == 50)
            {
                //Done
            }
        }
    }

    public void Break25Coal()
    {

    }
}
