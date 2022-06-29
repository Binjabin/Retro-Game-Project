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
        Debug.Log("particle collision");
        int collisionEventCount = particles.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while(i < collisionEventCount)
        {
            Vector3 pos = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up,normal);
            SpawnCollisionParticles(pos, rot);
            i++;
        }
        
    }

    private void SpawnCollisionParticles(Vector3 location, Quaternion rotation)
    {
        Vector3 ray = rotation * Vector3.up;
        Debug.Log("hit");
        Debug.DrawRay(location, ray, Color.green, 3);
        Instantiate(collisionEffect, location, rotation);
    }
}
