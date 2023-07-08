using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlataformPreset", menuName = "Plataform/Preset")]
public class Plataform_Preset : ScriptableObject
{

    //Jump parameters
    public float jumpHeight; //the height the jump will reach
    [Min(.2f)] public float timeToMaxHeight; //the time it will take to reach max height
    [Min(.2f)] public float timeToFall; //the time it will take to get to the ground from max jump height
    [HideInInspector]public float jumpSpeed;
    [HideInInspector]public float fallGravity, jumpGravity;
    [HideInInspector] public float terminalVelocity;

    //Movement parameters
    public float speed;

    public void CalculateParameters()
    {
        //the amount of physics tick per second
        float ticksPerSecond = (1f / Time.fixedDeltaTime) - 1;
        float tickOffset = ticksPerSecond * Time.fixedDeltaTime;

        //basic speed formula plus the extra force needed to compensate for the gravity force
        jumpSpeed = (jumpHeight / timeToMaxHeight);
        jumpSpeed *= 1f + ExtraForceWithDrag(jumpSpeed, (jumpSpeed / (ticksPerSecond * timeToMaxHeight)),
            ticksPerSecond - tickOffset, timeToMaxHeight);

        //gravity calculations. jump and fall gravity are different
        jumpGravity = (jumpSpeed / (ticksPerSecond * timeToMaxHeight));
        fallGravity = (jumpSpeed / (ticksPerSecond * timeToFall));
        terminalVelocity = fallGravity * (ticksPerSecond * timeToFall);

        //print($"jump: {jumpSpeed}");
        //print($"gravity: {gravity}");
    }


    /// <summary>
    /// Calculates the percentage of force needed to compensate some dictated drag force
    /// </summary>
    /// <param name="speed">the speed you want to calculate the compensation</param>
    /// <param name="dragForce">the force slowing down the speed</param>
    /// <param name="ticksPerSecond">the amount of ticks the drag is added per second</param>
    /// <param name="totalTime">the amount of time the calculation is taking into account</param>
    /// <returns></returns>
    float ExtraForceWithDrag(float speed, float dragForce, float ticksPerSecond, float totalTime)
    {
        float dragPercentage = dragForce / speed;
        dragPercentage *= (ticksPerSecond * totalTime);

        return dragPercentage;
    }
}
