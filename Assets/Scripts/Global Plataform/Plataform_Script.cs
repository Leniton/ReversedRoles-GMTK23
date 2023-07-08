using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsHandler))]
public class Plataform_Script : MonoBehaviour
{
#if UNITY_EDITOR
    public bool testMode = true;
#endif

    public enum State { idle, walking, jumping }
    [SerializeField] public State state = new State();

    public Vector3 input = Vector3.zero;
    public bool hasControl = true;
    [SerializeField] bool onGround;
    public bool canJump { get; private set; }
    public bool OnGround
    {
        get
        {
            return onGround;
        }
        private set { }
    }
    public Action OnGrounded;

    //Reference Parameters
    public PhysicsHandler physicsHandler;
    GameObject standingFloor;

    //Jump parameters
    public Plataform_Preset preset;
    [SerializeField]float coyoteTime = 0;
    float currentGravity;

    //Movement parameters
    Vector3 finalVelocity;
    [SerializeField]float maxSlopeAngle=45;

    void Awake()
    {
        physicsHandler = GetComponent<PhysicsHandler>();

        //adding events
        physicsHandler.CollisionEnter = CollisionEnter;
        physicsHandler.CollisionExit = CollisionExit;

        preset.CalculateParameters();
    }

    void FixedUpdate()
    {

#if UNITY_EDITOR
        if (testMode)
        {
            preset. CalculateParameters();
        }
#endif

        if (hasControl)
        {
            if(input.y > 0)
            {
                if (onGround || canJump)
                {
                    Jump();
                    input.y = 0;
                }
            }

            finalVelocity.x = input.x * preset.speed;

            physicsHandler.Velocity = finalVelocity;
        }

        Gravity();
    }

    void Gravity()
    {
        if (onGround) return;

        Vector2 gravityEffect = physicsHandler.Velocity;

        if (gravityEffect.y >= 0) currentGravity = preset.jumpGravity;
        else currentGravity = preset.fallGravity;

        //if (gravityEffect.y > -terminalVelocity)
        gravityEffect.y = Mathf.Clamp(gravityEffect.y - currentGravity, -preset.terminalVelocity, 9999999999);

        finalVelocity = gravityEffect;

        if (gravityEffect.y < 0)
        {
            input.y = 0;
            state = State.jumping;
        }
    }

    void Jump()
    {
        finalVelocity.y = preset.jumpSpeed;
    }

    void CollisionEnter(CollisionData data)
    {
        //print($"collided with {data.collider.gameObject.name}");
        if (data.collider.gameObject.CompareTag("Chão"))
        {
            if(Vector3.Angle(data.contacts[0].normal, Vector3.up) <= maxSlopeAngle)
            {
                standingFloor = data.collider.gameObject;
                onGround = true;
                state = input.x == 0 ? State.idle : State.walking;
                physicsHandler.Velocity = Vector3.zero;
                finalVelocity = physicsHandler.Velocity;

                OnGrounded?.Invoke();
            }
        }
    }
    void CollisionExit(CollisionData data)
    {
        //print($"Exit {data.collider.gameObject.name}");
        if (data.collider.gameObject == standingFloor)
        {
            standingFloor = null;
            onGround = false;
            StartCoroutine(CoyoteTime());
        }
    }

    IEnumerator CoyoteTime()
    {
        canJump = true;
        if (coyoteTime > 0) yield return new WaitForSeconds(coyoteTime);
        canJump = false;
    }

    #region Enable/Disable

    private void OnDisable()
    {
        physicsHandler.CollisionEnter = null;
        physicsHandler.CollisionExit = null;
    }

    #endregion
}
