using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COOL : MonoBehaviour
{
    private ParticleSystem ps;

    private ParticleSystem.Particle[] particles; //array of particles being controlled 

    [SerializeField] private float particleSpeed = 1;

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
        for(int i = 0; i< numParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];

            //apply things to particles particle
            Vector3 moveVector = mouseWorldPos - particle.position;

            particle.velocity += ((moveVector.normalized * particleSpeed) - particle.velocity) * Time.deltaTime;

            particles[i] = particle;
        }
        ps.SetParticles(particles);

    }
}
