using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Particles : MonoBehaviour
{
    ParticleSystem.Particle[] particles;
    ParticleSystem system;
    int numParticles;
    public Color32 interactionColor = new Color32(100, 200, 100, 255);
    public float interactionStrength = -10;

    public void ComputeDistance (Vector3 point)
    {
        numParticles = system.GetParticles(particles);
        for(int i = 0; i < numParticles; i++)
        {
            ParticleSystem.Particle particle = particles[i];
            point.z = transform.position.z;
            float distance = (point - particle.position).magnitude;
            if(distance < 2f) {
                particles[i].startColor = Color.Lerp(particles[i].GetCurrentColor(system), interactionColor, Map(distance, 0, 2, 1, 0));
                //particles[i].velocity = Vector3.MoveTowards(particle.position, point, 1);
                particles[i].velocity = (particle.position - point) *  Map(distance, 0, 2, interactionStrength, 0);
            } 
            
        }
        system.SetParticles(particles, numParticles);
    }

    // Use this for initialization
    void Start()
    {
        system = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[system.main.maxParticles];
    }

    private float Map(float value, float min, float max, float new_min, float new_max)
    {
        return new_min + (value - min) * (new_max - new_min) / (max - min);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.up * 0.1f);
/*        numParticles = system.GetParticles(particles);
        for(int i = 0; i < numParticles; i++)
        {
            particles[i].velocity += Vector3.MoveTowards(particles[i].position, transform.position, -0.0001f);
        }
        system.SetParticles(particles, numParticles);  */
    }
    
}
