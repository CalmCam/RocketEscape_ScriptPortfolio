using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlider : MonoBehaviour
{
    Vector3 startingPosition;
    public Vector3 movementVector;
    [Range(0,1)] public float movementFactor;
    public float period = 2f;

    InputManager inputManager;

    void Start()
    {
        startingPosition = transform.position;

        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // protecting against NaN error when period == 0
        
        if (inputManager.playing == true)
        {
            float cycles = Time.time / period; // continually growing over time

            const float tau = Mathf.PI * 2; // constant value of tau
            float rawSinWave = Mathf.Sin(cycles * tau); // going from 1 to -1

            movementFactor = (rawSinWave + 1) / 2; // recalculating so it is going from 1 to 0

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}
