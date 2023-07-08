using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Attack golpe, tiro;
    [SerializeField] HealthManager healthManager;
    [SerializeField] Plataform_Script plataform_Script;
    [SerializeField] Animator animator;
    int damageHash = Animator.StringToHash("Damaged");

    private void Awake()
    {
        golpe.attacker = gameObject;
        tiro.attacker = gameObject;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            golpe.StartAttack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tiro.StartAttack();
        }

        animator.SetBool(damageHash, healthManager.invincible);
    }
}
