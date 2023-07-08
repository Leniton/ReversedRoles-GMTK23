using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Script : MonoBehaviour
{
    [SerializeField] RectTransform hudHealth;
    [SerializeField] HealthSystem health;

    private void Awake()
    {
        health.healthReference.onChange += UpdateHealth;
    }

    void UpdateHealth()
    {
        hudHealth.sizeDelta = new Vector2(100 * health.healthReference.healthValue, 100);
    }
}
