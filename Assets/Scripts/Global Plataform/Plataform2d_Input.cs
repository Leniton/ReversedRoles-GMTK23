using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform2d_Input : MonoBehaviour
{
    [SerializeField] Plataform_Script plataform;
    [SerializeField] Plataform_Preset normal, gliding;
    [SerializeField] float cashedInputDuration;
    public bool isGliding = false;
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
        if (currentTime >= cashedInputDuration)
        {
            currentTime = 0;
            //cashedInput = Vector3.zero;
            cashedInput.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            plataform.preset = normal;
            plataform.input.y = 1;
            currentTime = 0;
        }
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (plataform.physicsHandler.Velocity.y < 0 && !plataform.canJump)
            {
                Glide();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            plataform.preset = normal;
            isGliding = true;
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
        isGliding = true;
    }

    void OnTouchGround()
    {
        plataform.preset = normal;
        isGliding = false;
        plataform.input = cashedInput;
    }
}
