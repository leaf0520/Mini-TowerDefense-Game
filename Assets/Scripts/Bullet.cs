using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;           
    public float speed = 10f;          
    public int damage = 10;            

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            
            Destroy(gameObject);
            return;
        }

       
        Vector2 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

       
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

       
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
      
        Health targetHealth = target.GetComponent<Health>();

        
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

       
        Destroy(gameObject);
    }
}
