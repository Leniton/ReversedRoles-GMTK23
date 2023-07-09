using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]//começar o hp cheio
public class HealthManager : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] bool instanced, disabledOnDeath = true;
    [SerializeField] float invencibilityDuration = 0;
    Health _health = new();
    public bool invincible{ get; private set; }
    Coroutine isInvincible;

    public Health health => instanced ? _health : healthSystem.reference;

    private void Awake()
    {
        healthSystem.reference.FullHeal();
        _health = healthSystem.reference;
        if (instanced) _health.onChange += OnHealthChange;
        else healthSystem.reference.onChange += OnHealthChange;
    }

    public void AddListener(Action callback)
    {
        if (instanced) _health.onChange += callback;
        else healthSystem.reference.onChange += callback;
    }

    public void RemoveListener(Action callback)
    {
        if (instanced) _health.onChange -= callback;
        else healthSystem.reference.onChange -= callback;
    }

    private void OnDestroy()
    {
        if (instanced) _health.onChange -= OnHealthChange;
        else healthSystem.reference.onChange -= OnHealthChange;
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

    public void HiddenSet(int value)
    {
        if (instanced) _health.HiddenSet(value);
        else healthSystem.reference.HiddenSet(value);
    }
    public void FullHeal()
    {
        if (instanced) _health.FullHeal();
        else healthSystem.reference.FullHeal();
    }

    void OnHealthChange()
    {
        int currentValue = instanced ? _health.Value : healthSystem.reference.Value;
        if (currentValue <= 0)
        {
            //Debug.Log($"Dead with {currentValue}");
            if (disabledOnDeath) gameObject.SetActive(false);
        }
    }

    public void InvincibleMode(float duration)
    {
        if (isInvincible == null)
            isInvincible = StartCoroutine(Invincible(duration));
    }

    IEnumerator Invincible(float duration)
    {
        if (duration <= 0) yield break;
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
        isInvincible = null;
    }
}
