using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatlhComponent : MonoBehaviour
{
    private Vector3 spawnPoint;
    [SerializeField] GameObject artifact;
    [SerializeField] float safeRadius;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, artifact.transform.position) > safeRadius)
        {
            transform.position = spawnPoint;
            artifact.transform.position = spawnPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstaculos"))
        {
            transform.position = spawnPoint;
            artifact.transform.position = spawnPoint;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(artifact.transform.position, safeRadius);
    }
}
