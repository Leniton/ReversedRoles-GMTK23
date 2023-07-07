using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform2d_Input : MonoBehaviour
{
    [SerializeField] Plataform_Script plataform;

    void Awake()
    {
        if (plataform) plataform.GetComponent<Plataform_Script>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            plataform.input.y = 1;
        }

        plataform.input.x = Input.GetAxis("Horizontal");
    }
}
