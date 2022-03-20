using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float thrust;
    public float rotateSpeed;
    public AudioClip thruster;
    public AudioClip sideThruster;
    public ParticleSystem mainThruster;
    public ParticleSystem rightSidethruster;
    public ParticleSystem leftSidethruster;

    // fuel
    public Slider fuelSlider;
    private float fuel = 100f;
    private float currentFuel;
    private float fuelBurnRate = 20f;
    private bool hasFuel = true;

    // speed boost

    private bool isBoosting = false;
    private float timer = 0.0f;
    public float timerLength = 3.0f;
    
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        currentFuel = fuel;
        rotateSpeed = 0;
        thrust = 0;
    }

    void Update()
    {
        fuelSlider.value = currentFuel / fuel;
        if (currentFuel <= 0)
        {
            hasFuel = false;
            GetComponent<Movement>().enabled = false;
        }

        ProcessRotation();
        ProcessThrust();
        SpeedBoost();
    }

    // pickups
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuel Pickup":
                currentFuel = 100;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                break;
            case "Speed Boost":
                isBoosting = true;
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                break;
        }
    }

    void SpeedBoost()
    {
        if (isBoosting)
        {
            timer += Time.deltaTime;
            thrust = 2000f;
        }
        else
        {
            timer = 0f;
        }
        if (timer >= timerLength)
        {
            isBoosting = false;
        }
    }

    // thrust and movement
    void ProcessThrust()
    {
        if (hasFuel && thrust == 1000)
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            mainThruster.Play();

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thruster);
            }

            currentFuel -= fuelBurnRate * Time.deltaTime;
        }
        else if (thrust == 0 && rotateSpeed == 0) 
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        RotationMethod();
        ApplyRotation(rotateSpeed);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }

    void RotationMethod()
    {
        if (rotateSpeed == 200)
        {
            rightSidethruster.Play();
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }

        else if (rotateSpeed == -200)
        {
            leftSidethruster.Play();
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }

        if (!audioSource.isPlaying && rotateSpeed == 200 || !audioSource.isPlaying && rotateSpeed == -200)
        {
            audioSource.PlayOneShot(sideThruster);
        }
    }
}
