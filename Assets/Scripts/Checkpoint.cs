using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameManager manager;
    [SerializeField] HealthSystem poder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(poder.reference.Value < 1)
            {
                poder.reference.Heal(1);
            }
            manager.ChangeCheckpoint(this);
        }
    }
}
