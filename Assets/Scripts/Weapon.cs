using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Camera playerCamera;
    public AudioClip gunFireSound;
    bool allowReset = true;
    public int currentBurst;
    public AudioSource audioSource;

    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefacLifeTime = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //shoot
        bullet
            .GetComponent<Rigidbody>()
            .AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        //Destroy after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefacLifeTime));
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(gunFireSound);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
