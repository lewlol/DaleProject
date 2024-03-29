using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float health;
    public PlayerStatsData stats;
    int regenTime;
    private void Start()
    {
        health = stats.maxhealth;

        StartCoroutine(SetHealthUI());

        CustomEventSystem.current.onSleep += FullHeal;
    }
    public void AddHealth(int healAmount)
    {
        health += healAmount;//Heal

        //Event For Health Gain
        CustomEventSystem.current.HealthChange(health, stats.maxhealth);

        //Check if Health is over MaxHealth
        if (health > stats.maxhealth)
        {
            health = stats.maxhealth;
        }
    }

    public void FullHeal()
    {
        health = stats.maxhealth;
        //Event For Health Gain
        CustomEventSystem.current.HealthChange(health, stats.maxhealth);
    }

    public void RegenerationEffect(int healAmount, int maxRegenTime)
    {
        StartCoroutine(AddHealthOverTime(healAmount, maxRegenTime));
    }
    public void TakeDamage(int damage)
    {
        health -= damage; //Remove Health

        //Event for Health Removal
        CustomEventSystem.current.HealthChange(health, stats.maxhealth);

        if (health <= 0) //Check Death
        {
            //Death
        }
    }

    IEnumerator AddHealthOverTime(int healAmount, int maxRegenTime)
    {
        AddHealth(healAmount);
        regenTime++;
        yield return new WaitForSeconds(1);
        if(regenTime < maxRegenTime)
        {
            RegenerationEffect(healAmount, maxRegenTime);
        }
        else
        {
            regenTime = 0;
        }
    }

    IEnumerator SetHealthUI()
    {
        yield return new WaitForSeconds(0.5f);
        CustomEventSystem.current.HealthChange(health, stats.maxhealth);
    }
}
