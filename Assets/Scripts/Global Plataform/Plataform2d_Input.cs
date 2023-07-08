using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform2d_Input : MonoBehaviour
{
    [SerializeField] Plataform_Script plataform;
    [SerializeField] Plataform_Preset normal, gliding;
    [SerializeField] float cashedInputDuration;
    float currentTime;
    Vector3 cashedInput;

    void Awake()
    {
        if (plataform) plataform.GetComponent<Plataform_Script>();
        plataform.OnGrounded += ChangePreset;
        normal.CalculateParameters();
        gliding.CalculateParameters();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (plataform.OnGround)
            {
                plataform.preset = normal;
                plataform.input.y = 1;
            }
            else
            {
                cashedInput.y = 1;
            }
        }
        currentTime += Time.deltaTime;
        if (currentTime >= cashedInputDuration)
        {
            currentTime = 0;
            cashedInput = Vector3.zero;
        }

        if (plataform.physicsHandler.Velocity.y < 0 && cashedInput.y > 0)
        {
            Glide();
        }

        plataform.input.x = Input.GetAxis("Horizontal");
    }

    void Glide()
    {
        plataform.preset = gliding;
        cashedInput.y = 0;
    }

    void ChangePreset()
    {
        plataform.preset = normal;
    }
}
