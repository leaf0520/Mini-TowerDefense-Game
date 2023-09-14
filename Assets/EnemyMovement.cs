using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3[] waypoints;
    public float speed = 5.0f;
    private int currentWaypointIndex = 0;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        if (waypoints.Length == 0) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], step);

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.1f)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                playerHealth.TakeDamage(1);
                Destroy(gameObject);
            }
            else
            {
                currentWaypointIndex++;
            }
        }
    }
}
