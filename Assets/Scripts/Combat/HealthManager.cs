using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] bool instanced;
    [SerializeField] float invencibilityDuration = 0;
    Health _health;
    public bool invincible{ get; private set; }

    public Health health => instanced ? _health : healthSystem.health;

    private void Awake()
    {
        healthSystem.health.FullHeal();
        _health = healthSystem.health;
        if (instanced) _health.onChange += OnHealthChange;
        else healthSystem.health.onChange += OnHealthChange;
    }

    public void DealDamage(int value)
    {
        if (invincible) return;
        InvincibleMode(invencibilityDuration);
        if (instanced)
        {
            _health.Damage(value);
            return;
        }
        healthSystem.health.Damage(value);
    }

    void OnHealthChange()
    {
        int currentValue = instanced ? _health.health : healthSystem.health.health;

        if (currentValue <= 0)
        {
            Debug.Log("Dead");
            gameObject.SetActive(false);
        }
    }

    public void InvincibleMode(float duration)
    {
        StartCoroutine(Invincible(duration));
    }

    IEnumerator Invincible(float duration)
    {
        if (duration <= 0) yield break;
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }
}
