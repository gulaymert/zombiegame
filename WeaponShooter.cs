using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public GameObject neonHalkaPrefab; 
    public Transform firePoint;     
    public float fireRate = 0.3f;  
    private float nextFireTime = 0f;

    void Update()
    {
       
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        
        Instantiate(neonHalkaPrefab, firePoint.position, firePoint.rotation);
        
        
    }
}