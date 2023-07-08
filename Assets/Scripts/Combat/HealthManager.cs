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

    public Health health => instanced ? _health : healthSystem.reference;

    private void Awake()
    {
        healthSystem.reference.FullHeal();
        _health = healthSystem.reference;
        if (instanced) _health.onChange += OnHealthChange;
        else healthSystem.reference.onChange += OnHealthChange;
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
        healthSystem.reference.Damage(value);
    }

    void OnHealthChange()
    {
        int currentValue = instanced ? _health.Value : healthSystem.reference.Value;

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
