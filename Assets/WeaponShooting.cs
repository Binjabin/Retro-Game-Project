using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
public class WeaponShooting : NetworkBehaviour
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
    bool isMine;
    NetworkPlayer myPlayer;
    NetworkPlayer thisPlayer;
    [SerializeField] Slider slider;

    public override void Spawned()
    {
        particles = GetComponent<ParticleSystem>();
        currentEnergy = maxEnergy;
        myPlayer = NetworkManager.Instance.GetPlayer();
        thisPlayer = GetComponentInParent<PlayerMovement>().networkPlayer;
        if (myPlayer == thisPlayer)
        {
            isMine = true;
        }
        else
        {
            isMine = false;
        }

    }
    private void OnParticleCollision(GameObject other)
    {
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

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    void ToggleShooting(bool isShootingNow, NetworkPlayer playerToToggle)
    {
        if(thisPlayer == playerToToggle && myPlayer != thisPlayer)
        {
            isShooting = isShootingNow;
        }
    }

    private void Update()
    {
        if (isMine)
        {
            if (currentEnergy > 0f)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else
            {
                isShooting = false;
            }

            if (isShooting)
            {

                Debug.Log("Shooting");
                if (!particles.isPlaying)
                {
                    particles.Play();
                    ToggleShooting(true, myPlayer);
                }

                timeSinceShoot = 0f;
                currentEnergy -= Time.deltaTime * energyUsePerSecond;
            }
            else
            {
                Debug.Log("Stop Shooting");
                if (particles.isPlaying)
                {
                    particles.Stop();
                    ToggleShooting(false, myPlayer);
                }

                timeSinceShoot += Time.deltaTime;
                if (timeSinceShoot > timeUntilRegenStart)
                {
                    currentEnergy += Time.deltaTime * energyRegenPerSecond;
                }

            }
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
        }
        else
        {
            if (isShooting)
            {
                Debug.Log("Shooting");
                if (!particles.isPlaying)
                {
                    particles.Play();

                }

                timeSinceShoot = 0f;
                currentEnergy -= Time.deltaTime * energyUsePerSecond;
            }
            else
            {
                Debug.Log("Stop Shooting");
                if (particles.isPlaying)
                {
                    particles.Stop();

                }

                timeSinceShoot += Time.deltaTime;
                if (timeSinceShoot > timeUntilRegenStart)
                {
                    currentEnergy += Time.deltaTime * energyRegenPerSecond;
                }

            }
        }
        
    }
}
