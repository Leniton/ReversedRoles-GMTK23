using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Attack golpe, tiro;
    [SerializeField] HealthManager healthManager;
    [SerializeField] HealthSystem barraDePoder;
    [SerializeField] Plataform_Script plataform_Script;
    [SerializeField] Plataform2d_Input plataformInput;
    [SerializeField] Animator animator;
    int damageHash = Animator.StringToHash("Damaged");
    int attackHash = Animator.StringToHash("Attack");
    int glideHash = Animator.StringToHash("Gliding");

    private void Awake()
    {
        golpe.attacker = gameObject;
        tiro.attacker = gameObject;
        barraDePoder.reference.FullHeal();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            golpe.StartAttack();
            animator.Play(attackHash);
        }
        if (Input.GetKeyDown(KeyCode.E) && barraDePoder.reference.Value > 0)
        {
            barraDePoder.reference.Damage(1);
            tiro.StartAttack();
            animator.Play(attackHash);
        }

        animator.SetBool(damageHash, healthManager.invincible);
        animator.SetBool(glideHash, plataformInput.isGliding);
    }
}
