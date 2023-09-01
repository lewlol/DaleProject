using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float health;
    public PlayerStatsData stats;

    public void AddHealth(int healAmount)
    {
        health += healAmount;//Heal

        //Event For Health Gain
        CustomEventSystem.current.PlayerDamaged();

        //Check if Health is over MaxHealth
        if (health > stats.maxhealth)
        {
            health = stats.maxhealth;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage; //Remove Health

        //Event for Health Removal
        CustomEventSystem.current.PlayerDamaged();

        if (health <= 0) //Check Death
        {
            //Death
        }
    }
}
