using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] HealthSystem targetHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetHealth.reference.Heal(1);
        Destroy(gameObject);
    }
}
