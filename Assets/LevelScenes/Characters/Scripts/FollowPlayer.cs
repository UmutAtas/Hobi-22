using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float followOffset;
    [SerializeField] private float rotationSpeed;
    private Quaternion targetRotation;
    private float distance;
    
    private void LateUpdate()
    {
        PlayerFollow();
    }

    private void PlayerFollow()
    {
        var playerPos = PlayerController.Instance.transform.position;
        targetRotation = Quaternion.LookRotation(playerPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        distance = Vector3.Distance(transform.position, playerPos);
        if (distance > followOffset)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, playerPos, followSpeed * Time.deltaTime);     
        }
    }
}
