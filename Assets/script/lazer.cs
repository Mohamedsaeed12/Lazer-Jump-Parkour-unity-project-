using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera playerCamera; // Reference to the player's camera
    public Transform firePoint; // The visual starting point of the laser
    public float laserDuration = 0.5f;
    public float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(FireLaser());
        }
    }

    IEnumerator FireLaser()
    {
        RaycastHit hit;
        // Raycast from the camera's position in the forward direction
        if (Physics.Raycast(playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit, range))
        {
            lineRenderer.SetPosition(0, firePoint.position); // Start the laser from the fire point
            lineRenderer.SetPosition(1, hit.point); // End the laser at the hit point

            if (hit.collider.CompareTag("Destroyable")) // Check if the hit object is destroyable
            {
                Destroy(hit.collider.gameObject); // Destroy the object
            }
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, playerCamera.transform.position + playerCamera.transform.forward * range);
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        lineRenderer.enabled = false;
    }
}
