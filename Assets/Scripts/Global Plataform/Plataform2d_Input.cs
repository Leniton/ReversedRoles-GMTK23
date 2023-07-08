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
    Vector3 baseScale;

    void Awake()
    {
        if (plataform) plataform.GetComponent<Plataform_Script>();
        plataform.OnGrounded += OnTouchGround;
        normal.CalculateParameters();
        gliding.CalculateParameters();
        baseScale = transform.localScale;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (plataform.OnGround)
            {
                plataform.preset = normal;
                plataform.input.y = 1;
            }
            cashedInput.y = 1;
            currentTime = 0;
        }
        if (currentTime >= cashedInputDuration)
        {
            currentTime = 0;
            //cashedInput = Vector3.zero;
            cashedInput.y = 0;
        }

        if (plataform.physicsHandler.Velocity.y < 0 && cashedInput.y > 0)
        {
            Glide();
        }

        plataform.input.x = Input.GetAxis("Horizontal");
        cashedInput.x = plataform.input.x == 0 ? cashedInput.x : plataform.input.x;

        Vector3 finalScale = baseScale;
        finalScale.x = baseScale.x * Mathf.Sign(cashedInput.x);
        transform.localScale = finalScale;
    }

    void Glide()
    {
        plataform.preset = gliding;
    }

    void OnTouchGround()
    {
        plataform.preset = normal;
        plataform.input = cashedInput;
    }
}
