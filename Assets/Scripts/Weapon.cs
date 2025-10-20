using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public AudioClip gunFireSound;
    bool allowReset = true;
    public int currentBurst;
    public AudioSource audioSource;
    public Transform bulletSpawn;
    public Transform cameraTransform;
    public float bulletSpeed = 100f;
    public float delayTime = 0.1f;
    public GameObject bulletTrail;
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
        GameObject trail = Instantiate(bulletTrail, bulletSpawn.position, transform.rotation);
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo))
        {
            StartCoroutine(SpawnTrail(trail, hitInfo.point));
            if (hitInfo.collider.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
            {
                Debug.Log("Hit a player!");
                playerStats.Damage(1);
            }
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, cameraTransform.position + cameraTransform.forward * 1000f));
        }
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(gunFireSound);
    }

    private IEnumerator SpawnTrail(GameObject trail, Vector3 endPosition)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;
        endPosition = endPosition + (endPosition - startPosition).normalized * 0.5f;
        float trailTime = Vector3.Distance(startPosition, endPosition) / bulletSpeed;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            time += Time.deltaTime / trailTime;
            yield return null;
        }
        Destroy(trail.gameObject);
    }
}
