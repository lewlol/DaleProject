using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
    private void OnMouseEnter()
    {
        HighlightBlock();
    }

    private void OnMouseExit()
    {
        DeHighlightBlock();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            BreakingBlock();
        }
    }
    void BreakingBlock()
    {

    }

    void BreakBlock()
    {

    }

    void HighlightBlock()
    {

    }

    void DeHighlightBlock()
    {

    }
}
