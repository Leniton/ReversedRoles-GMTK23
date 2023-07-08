using System;
using UnityEngine;

[CreateAssetMenu(fileName ="newHealth",menuName ="Health")]
public class HealthSystem : ScriptableObject
{
    public Health health;
}

[Serializable]
public struct Health
{
    [SerializeField] int MaxHealth;
    private int _health;

    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            onChange?.Invoke();
        }
    }

    public Action onChange;
    public void Damage(int value) => health -= value;
    public void Heal(int value) => health += Mathf.Clamp(value, 0, MaxHealth - health);
    public void FullHeal() => health = MaxHealth;
}