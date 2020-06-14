using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class Fire1 : MonoBehaviour
{
    private ParticleSystem hitParticles;

    void Awake()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            hitParticles.Play();
        }
    }

    void PlayParticles()
    {
        hitParticles.Play();
    }
}
