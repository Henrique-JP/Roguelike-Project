using UnityEngine;

public class SimpleShoot : WeaponFireMode
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;

    private float lastFireTime;

    public override void Fire(Transform firePoint, Vector2 direction)
    {
        if (Time.time < lastFireTime + fireRate)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        lastFireTime = Time.time;
    }
}
