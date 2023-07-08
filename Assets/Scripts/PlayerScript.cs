using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Attack golpe, tiro;
    [SerializeField] HealthManager healthManager;
    [SerializeField] HealthSystem barraDePoder;
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
        if (Input.GetKeyDown(KeyCode.E) && barraDePoder.reference.Value > 0)
        {
            barraDePoder.reference.Damage(1);
            tiro.StartAttack();
        }

        animator.SetBool(damageHash, healthManager.invincible);
    }
}
