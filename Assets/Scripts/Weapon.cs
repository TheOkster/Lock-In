using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Weapon : NetworkBehaviour
{
    public AudioClip gunFireSound;
    bool allowReset = true;
    public int currentBurst;
    public AudioSource audioSource;
    public Transform bulletSpawn;
    public Transform cameraTransform;
    public float delayTime = 0.1f;
    public Bullet bulletTrail;
    private float lastFireTime = -0.1f;

    // Update is called once per frame
    public void PressShoot()
    {
        if (Time.time - lastFireTime > delayTime)
        {
            FireWeapon();
            lastFireTime = Time.time;
        }
    }

    private void FireWeapon()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo))
        {
            spawnTrailServerRpc(bulletSpawn.position, transform.rotation, hitInfo.point);
            if (hitInfo.collider.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
            {
                Debug.Log("Hit a player!");
                playerStats.damageServerRpc(1);
            }
        }
        else
        {
            spawnTrailServerRpc(bulletSpawn.position, transform.rotation, cameraTransform.position + cameraTransform.forward * 1000f);
        }
        audioSource.PlayOneShot(gunFireSound);
        shootSoundServerRpc(audioSource.transform.position);
    }

    [ServerRpc] void spawnTrailServerRpc(Vector3 startPosition, Quaternion rotation, Vector3 endPosition)
    {
        endPosition = endPosition + (endPosition - startPosition).normalized * 0.5f;

        Bullet trail = Instantiate(bulletTrail, bulletSpawn.position, transform.rotation);
        trail.startPosition.Value = trail.transform.position;
        trail.endPosition.Value = endPosition;
        trail.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc] void shootSoundServerRpc(Vector3 location) 
    {
        shootSoundClientRpc(location);
    }

    [ClientRpc] void shootSoundClientRpc(Vector3 location) 
    {
        if (IsOwner) return;
        AudioSource.PlayClipAtPoint(gunFireSound, location);
    }
}
