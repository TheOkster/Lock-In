using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Camera playerCamera;
<<<<<<< HEAD
    public AudioClip gunFireSound;
    bool allowReset = true;
    public int currentBurst;
    public AudioSource audioSource;
=======

    bool allowReset = true;
    public int currentBurst;
>>>>>>> 852054d (Added crosshair, walking, and a new map scene)

    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefacLifeTime = 3f;

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (Input.GetKey(KeyCode.Mouse0) && Time.time - lastFireTime > delayTime)
        {
=======
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
>>>>>>> 852054d (Added crosshair, walking, and a new map scene)
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
<<<<<<< HEAD
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
=======
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //shoot
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        //Destroy after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefacLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
>>>>>>> 852054d (Added crosshair, walking, and a new map scene)
    }
}
