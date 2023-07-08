using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Script : MonoBehaviour
{
    [SerializeField] RectTransform hudHealth;
    [SerializeField] HealthSystem health;

    [SerializeField] RectTransform hudPoder;
    [SerializeField] HealthSystem poder;

    private void Awake()
    {
        health.reference.onChange += UpdateHealth;
        poder.reference.onChange += UpdatePoder;
    }

    void UpdateHealth()
    {
        hudHealth.sizeDelta = new Vector2(100 * health.reference.Value, 100);
    }

    void UpdatePoder()
    {
        hudPoder.sizeDelta = new Vector2(150 * poder.reference.Value, 150);
    }
}
