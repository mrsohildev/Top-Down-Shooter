using UnityEngine;

public class shootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;     // Visual Bullet Prefab
    public Transform firePoint;         // Gun se fire hota point
    public float bulletSpeed = 50f;     // Visual bullet ki speed  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left click / Touch
        {
            Shoot();
        }
    }

    void Shoot()
    {        
        // 2. VISUAL BULLET KO SPAWN KARNA (Travel Animation)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Bullet ko forward me bhejna
        
       
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, 10f*Time.deltaTime); // 2 seconds baad destroy
    }
} 
