using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]//-2 � para ser antes de aplicar qualquer valor
public class HUD_Script : MonoBehaviour
{
    [SerializeField] RectTransform hudHealth;
    [SerializeField] HealthSystem health;
    Vector2 hpBaseSize;

    [SerializeField] RectTransform hudPoder;
    [SerializeField] HealthSystem poder;
    Vector2 poderBaseSize;

    private void Awake()
    {
        hpBaseSize = hudHealth.sizeDelta;
        poderBaseSize = hudPoder.sizeDelta;

        health.reference.onChange += UpdateHealth;
        poder.reference.onChange += UpdatePoder;
    }

    void UpdateHealth()
    {
        int value = health.reference.Value;
        hudHealth.sizeDelta = new Vector2(hpBaseSize.x * value, hpBaseSize.y);
    }

    void UpdatePoder()
    {
        int value = poder.reference.Value;
        hudPoder.sizeDelta = new Vector2(poderBaseSize.x * value, poderBaseSize.y);
    }
}
