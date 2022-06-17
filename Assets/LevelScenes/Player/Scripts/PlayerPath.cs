using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private float roadDetectionRadius;

    private void Update()
    {
        GetMap();
    }

    private void GetMap()
    {
        Collider[] roadColliders = Physics.OverlapSphere(transform.position, roadDetectionRadius, roadLayer);
        foreach (var collider in roadColliders)
        {
            if (collider.gameObject != gameObject)
            {
                collider.GetComponent<PlatformMovement>().MovePlatform();
            }
        }
    }
}
