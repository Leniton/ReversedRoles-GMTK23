using System;
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
    float currentGravity;

    //Movement parameters
    Vector3 finalVelocity;
    float maxSlopeAngle;

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
                if (onGround)
                {
                    checkStopTime = true;
                    stopTime = 0;
                    Jump();
                    //print($"Start Height:{transform.position.y}");
                    //StartCoroutine(TestCount());
                    input.y = 0;
                }
            }

            finalVelocity.x = input.x * preset.speed;

            physicsHandler.Velocity = finalVelocity;
        }

        Gravity();
    }

    float t_initialHeight;

    //test parameters
    bool checkStopTime;
    float stopTime;
    float speedLost = 0;
    void Gravity()
    {
        if (onGround) return;

        Vector2 gravityEffect = physicsHandler.Velocity;

        if (gravityEffect.y >= 0) currentGravity = preset.jumpGravity;
        else currentGravity = preset.fallGravity;

        //if (gravityEffect.y > -terminalVelocity)
        gravityEffect.y = Mathf.Clamp(gravityEffect.y - currentGravity, -preset.terminalVelocity, 9999999999);

        finalVelocity = gravityEffect;

        //tests
        if (checkStopTime)
        {
            stopTime += Time.fixedDeltaTime;
            speedLost += preset.jumpSpeed - physicsHandler.Velocity.y;
        }

        if (gravityEffect.y <= 0)
        {
            if (checkStopTime)
            {
                checkStopTime = false;
                //print($"stop time: {stopTime} | height: {transform.position.y-t_initialHeight}");
                //print($"Initial speed: {jumpSpeed} | total speed lost: {speedLost}");
                //float ticksPerSecond = (1f / Time.fixedDeltaTime) - 1;
                //print($"Percentage lost: {ExtraForceWithDrag(jumpSpeed, jumpGravity, ticksPerSecond-1, timeToMaxHeight):0.0000}");
                stopTime = 0;
                speedLost = 0;
            }
        }

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
            standingFloor = data.collider.gameObject;
            onGround = true;
            state = input.x == 0 ? State.idle : State.walking;
            OnGrounded?.Invoke();
        }
    }
    void CollisionExit(CollisionData data)
    {
        //print($"Exit {data.collider.gameObject.name}");
        if (data.collider.gameObject == standingFloor)
        {
            standingFloor = null;
            onGround = false;
        }
    }

    #region Enable/Disable

    private void OnDisable()
    {
        physicsHandler.CollisionEnter = null;
        physicsHandler.CollisionExit = null;
    }

    #endregion
}
