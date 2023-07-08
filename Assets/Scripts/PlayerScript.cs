using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    void Start()
    {
        //playerHealth = healthSystem.health;
        healthSystem.health.FullHeal();
        healthSystem.health.Damage(1);
    }
}
