using System;
using UnityEngine;

[CreateAssetMenu(fileName ="newHealth",menuName ="Health")]
public class HealthSystem : ScriptableObject
{
    public Health reference = new Health();
}

[Serializable]
public struct Health
{
    [SerializeField] int MaxHealth;
    [SerializeField] private int health;

    public int Value
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
    public void Damage(int value)=> Value -= value;
    public void Heal(int value) => Value += Mathf.Clamp(value, 0, MaxHealth - Value);
    public void Set(int value) => Value = value;
    public void HiddenSet(int value) => health = value;//don't trigger event
    public void FullHeal() => Value = MaxHealth;
}