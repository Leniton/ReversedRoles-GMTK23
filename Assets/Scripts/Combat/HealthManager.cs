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

    public Health health => instanced ? _health : healthSystem.healthReference;

    private void Awake()
    {
        healthSystem.healthReference.FullHeal();
        _health = healthSystem.healthReference;
        if (instanced) _health.onChange += OnHealthChange;
        else healthSystem.healthReference.onChange += OnHealthChange;
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
        healthSystem.healthReference.Damage(value);
    }

    void OnHealthChange()
    {
        int currentValue = instanced ? _health.healthValue : healthSystem.healthReference.healthValue;

        if (currentValue <= 0)
        {
            Debug.Log($"Dead with {currentValue}");
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
