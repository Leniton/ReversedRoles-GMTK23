using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] bool instanced;
    Health health;
    private void Awake()
    {
        healthSystem.health.FullHeal();
        health = healthSystem.health;
        if (instanced) health.onChange += OnHealthChange;
        else healthSystem.health.onChange += OnHealthChange;
    }

    public void DealDamage(int value)
    {
        if (instanced)
        {
            health.Damage(value);
            return;
        }
        healthSystem.health.Damage(value);
    }

    void OnHealthChange()
    {
        int currentValue = instanced ? health.health : healthSystem.health.health;

        if (currentValue <= 0)
        {
            Debug.Log("Dead");
            gameObject.SetActive(false);
        }
    }
}
