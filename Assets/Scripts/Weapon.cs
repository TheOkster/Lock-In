using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioClip gunFireSound;
    bool allowReset = true;
    public int currentBurst;
    public AudioSource audioSource;

    public Transform bulletSpawn;
    public float bulletSpeed = 100f;
    public float delayTime = 0.1f;
    public TrailRenderer bulletTrail;
    private float lastFireTime = -0.1f;

    // Update is called once per frame
    public void PressShoot(){
        if (Time.time - lastFireTime > delayTime) {
            FireWeapon();
            lastFireTime = Time.time;
        }
    }

    private void FireWeapon()
    {
        TrailRenderer trail = Instantiate(bulletTrail, bulletSpawn.position, transform.rotation);
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out RaycastHit hitInfo))
        {
            StartCoroutine(SpawnTrail(trail, hitInfo));
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, null));
        }
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(gunFireSound);
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit? hitInfo)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        Vector3 endPosition;
        if (hitInfo.HasValue)
        {
            endPosition = hitInfo.Value.point;
        }
        else
        {
            endPosition = trail.transform.position + trail.transform.forward * 1000f;
        }
        float trailTime = Vector3.Distance(startPosition, endPosition) / bulletSpeed;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            time += Time.deltaTime / trailTime;
            yield return null;
        }
        trail.transform.position = endPosition;
        if (hitInfo.HasValue)
        {
            // Add impact effect here
            Debug.Log("Hit: " + hitInfo.Value.collider.name);
        }
        Destroy(trail.gameObject);
    }
}
