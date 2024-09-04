using UnityEngine;

public class BlasterFire : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint; // Assign a child GameObject positioned at the barrel end
    public Camera playerCamera;
    public float projectileSpeed = 1000f;
    public float projectileLifeTime = 2f; // Projectile disappears after 2 seconds

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Fire on left-click
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector3 firingDirection = playerCamera.transform.forward;
        projectile.transform.forward = firingDirection; // Ensure projectile faces the right direction
        projectile.GetComponent<Rigidbody>().AddForce(firingDirection * projectileSpeed);
        Destroy(projectile, projectileLifeTime); // Destroy the projectile after some time
    }
}

