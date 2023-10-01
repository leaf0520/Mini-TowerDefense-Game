using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;          
    public float rotationSpeed = 5f; 
    public Transform firePoint;      
    public GameObject bulletPrefab;  
    public float fireRate = 1f;     
    private float fireCooldown = 0f; 
    void Update()
    {
        
        if (target != null)
        {
            
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }

            
            fireCooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
       
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

      
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Seek(target);
        }
    }
}