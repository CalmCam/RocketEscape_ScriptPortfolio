using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool playing = false;

    Movement getMovement;

    // Start is called before the first frame update
    void Start()
    {
        getMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    public void Thrust(float thrustValue)
    {
        getMovement.thrust = thrustValue;
    }

    public void RotateSpeed(float rotateSpeedValue)
    {
        getMovement.rotateSpeed = rotateSpeedValue;
    }

    // Start of Level
    public void IsPlaying()
    {
        playing = true;
    }
}
