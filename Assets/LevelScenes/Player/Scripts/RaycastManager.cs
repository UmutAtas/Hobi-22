using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [NonSerialized] public Vector3 rayHitPoint;
    [SerializeField] private float rayOffsetFactor = 1.1f;

    public void RayCaster()
    {
        var ray = PlayerController.Instance.RayDirection;
        RaycastHit hit;
        if (Physics.Raycast(transform.position ,ray ,out hit ,Mathf.Infinity,1<<8))
        {
            rayHitPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z) -
                         ray * rayOffsetFactor;
        }
    }
}
