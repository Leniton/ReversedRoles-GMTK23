using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attacker;
    [SerializeField] int damage;
    [SerializeField] PhysicsHandler AttackObject;
    [SerializeField] Vector2 movingSpeed;
    [SerializeField] float duration = .4f;
    [SerializeField] bool canMultiHit = false;
    [SerializeField,Tooltip("deixar nulo se o ataque não seguir nenhum transform")] Transform parentWhileAttacking;// deixar nulo se o ataque não seguir nenhum transform
    List<GameObject> hitTargets = new();
    Vector3 startingPosition;

    private void Awake()
    {
        startingPosition = AttackObject.transform.localPosition;
        AttackObject.TriggerEnter += Hit;
    }

    public void StartAttack()
    {
        AttackObject.transform.localPosition = startingPosition;
        AttackObject.transform.SetParent(parentWhileAttacking);
        AttackObject.gameObject.SetActive(true);
        Vector2 ajustedSpeed = movingSpeed;
        ajustedSpeed.x *= Mathf.Sign(transform.lossyScale.x);
        ajustedSpeed.y *= Mathf.Sign(transform.lossyScale.y);
        AttackObject.Velocity = ajustedSpeed;
        StartCoroutine(StopAttack());
    }

    private void Hit(ColliderData collision)
    {
        if (collision.gameObject == attacker) return;
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health)
        {
            if (canMultiHit || (!canMultiHit && !hitTargets.Contains(collision.gameObject)))
            {
                Debug.Log("hit");
                if(!canMultiHit) hitTargets.Add(collision.gameObject);
                health.DealDamage(damage);
            }
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(duration);

        AttackObject.transform.SetParent(transform);
        AttackObject.transform.localPosition = startingPosition;
        AttackObject.gameObject.SetActive(false);
        AttackObject.Velocity = Vector2.zero;
        hitTargets.Clear();
    }
}
