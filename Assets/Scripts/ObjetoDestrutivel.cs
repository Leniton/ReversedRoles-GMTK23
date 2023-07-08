using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDestrutivel : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    [SerializeField] int autoHeal;

    private void Awake()
    {
        healthManager.AddListener(HitKill);
        healthManager.HiddenSet(autoHeal);
    }
    private void OnDestroy()
    {
        healthManager.RemoveListener(HitKill);
    }

    void HitKill()
    {
        if (healthManager.health.Value > 0)
        {
            healthManager.HiddenSet(autoHeal);
        }
    }
}
