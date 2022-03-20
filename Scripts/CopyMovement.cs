using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyMovement : MonoBehaviour
{
    public float thrust = 1000;
    public float rotateSpeed = 200;
    public AudioClip thruster;
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
    }

    void Update()
    {
        fuelSlider.value = currentFuel / fuel;
        if (currentFuel <= 0)
        {
            hasFuel = false;
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
        Debug.Log(timer);

        if (isBoosting)
        {
            timer += Time.deltaTime;
            thrust = 2000f;
        }
        else
        {
            timer = 0f;
            thrust = 1000f;
        }
        if (timer >= timerLength)
        {
            isBoosting = false;
        }
    }

    // thrust and movement
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) && hasFuel)
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            mainThruster.Play();

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thruster);
            }

            currentFuel -= fuelBurnRate * Time.deltaTime;
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        RotationMethod();
    }

    public void RotationMethod()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rightSidethruster.Play();
            ApplyRotation(rotateSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            leftSidethruster.Play();
            ApplyRotation(-rotateSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }
}
