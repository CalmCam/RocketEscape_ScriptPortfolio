using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessParticles : MonoBehaviour
{
    public ParticleSystem successParticles;

    bool isTransitioning = false;

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Player":
                successParticles.Play();
                break;
        }
    }

}
