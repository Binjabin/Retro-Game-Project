using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponShooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject collisionEffect;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    ParticleSystem particles;

    [SerializeField] float maxEnergy;
    [SerializeField] float energyUsePerSecond;
    float currentEnergy;
    [SerializeField] float timeUntilRegenStart;
    float timeSinceShoot;
    [SerializeField] float energyRegenPerSecond;
    [SerializeField] 
    bool isShooting;

    [SerializeField] Slider slider;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        currentEnergy = maxEnergy;
    }
    private void OnParticleCollision(GameObject other)
    {;
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
        Debug.DrawRay(location, ray, Color.green, 3);
        Instantiate(collisionEffect, location, rotation);
    }

    private void Update()
    {
        
        if(currentEnergy < 0.01f)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            isShooting = false;
        }
        
        if(isShooting)
        {
            particles.Play();
            timeSinceShoot = 0f;
            currentEnergy -= Time.deltaTime * energyUsePerSecond;
        }
        else
        {
            particles.Stop();
            timeSinceShoot += Time.deltaTime;
            if(timeSinceShoot > timeUntilRegenStart)
            {
                currentEnergy += Time.deltaTime * energyRegenPerSecond;
            }

        }
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
    }
}
