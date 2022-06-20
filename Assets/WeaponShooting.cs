using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject collisionEffect;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        int collisionEventCount = particles.GetCollisionEvents(other, collisionEvents);
        int i;
        while(i < collisionEventCount)
        {
            Vector3 pos = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up,normal);
            //SpawnCollisionParticles()
            i++;
        }
        
    }

    private void SpawnCollisionParticles(Vector3 location, float rotation)
    {
        Instantiate(collisionEffect, location, Quaternion.Euler(0f, 0f, rotation));
    }
}
