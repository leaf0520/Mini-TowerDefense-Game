using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 5.0f;
    public float fireRate = 1.0f;
    public float turnRate = 5.0f;
    public GameObject laserPrefab;
    public Transform firePoint;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject currentTarget;
    private float fireCooldown;

    private void Update()
    {
        UpdateEnemiesInRange();
        ChooseTarget();
        AimAtTarget();
        Fire();
    }

    private void UpdateEnemiesInRange()
    {
        enemiesInRange.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void ChooseTarget()
    {
        currentTarget = null;

        if (enemiesInRange.Count > 0)
        {
            currentTarget = enemiesInRange[0];
        }
    }

    private void AimAtTarget()
    {
        if (currentTarget == null) return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        float singleStep = turnRate * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void Fire()
    {
        if (currentTarget == null) return;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                    fireCooldown = 1 / fireRate;
                }
            }
        }
    }
}


