using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        //bal dameed
        CustomEventSystem.current.PlayerDamaged();
    }
}
