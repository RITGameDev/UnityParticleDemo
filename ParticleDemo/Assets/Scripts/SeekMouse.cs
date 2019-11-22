using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SeekMouse : MonoBehaviour
{
    private ParticleSystem ps;

    private ParticleSystem.Particle[] particles; //array of particles being controlled 

    [SerializeField] private float particleSpeed = 1;

    [SerializeField] private Color32 close;
    [SerializeField] private Color32 far;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        int numParticles = ps.GetParticles(particles);
        for (int i = 0; i < numParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];

            Vector3 moveVector = mouseWorldPos - particle.position;

            particle.velocity += ((moveVector.normalized * particleSpeed) - particle.velocity) * Time.deltaTime;

            particle.color = moveVector.magnitude < 2 ? close : far; 

            particles[i] = particle; //set the particle's data back into particles array
        }

        ps.SetParticles(particles, numParticles); //apply changes to particle system
    }
}
