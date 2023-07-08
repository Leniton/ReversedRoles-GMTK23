using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinhos : MonoBehaviour
{
    List<GameObject> hitTargets = new();
    [SerializeField] float delay = .2f;
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject == attacker) return;
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health)
        {
            if (!hitTargets.Contains(collision.gameObject))
            {
                hitTargets.Add(collision.gameObject);
                StartCoroutine(DamageTick(health));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hitTargets.Contains(collision.gameObject))
        {
            hitTargets.Remove(collision.gameObject);
        }
    }

    IEnumerator DamageTick(HealthManager healthManager)
    {
        WaitForSeconds seconds = new WaitForSeconds(delay);
        do
        {
            healthManager.DealDamage(damage);
            yield return seconds;

        } while (hitTargets.Contains(healthManager.gameObject));
    }
}
