using System;
using UnityEngine;

[CreateAssetMenu(fileName ="newHealth",menuName ="Health")]
public class HealthSystem : ScriptableObject
{
    public Health healthReference;
}

[Serializable]
public struct Health
{
    [SerializeField] int MaxHealth;
    [SerializeField] private int health;

    public int healthValue
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            onChange?.Invoke();
        }
    }

    public Action onChange;
    public void Damage(int value) => healthValue -= value;
    public void Heal(int value) => healthValue += Mathf.Clamp(value, 0, MaxHealth - healthValue);
    public void FullHeal() => healthValue = MaxHealth;
}