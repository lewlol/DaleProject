using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    //Int for each resource (Such a stupid way to do this but idc inventories are smelly)
    public int[] resources;

    public void AddResource(int id, int amount)
    {
        resources[id] += amount;
    }

    public void RemoveResource(int amount)
    {
        //Remove Resource
    }
}
