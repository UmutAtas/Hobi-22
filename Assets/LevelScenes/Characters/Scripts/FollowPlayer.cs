using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float followOffsetZ;
    [SerializeField] private float followSpeed;

    private void Update()
    {
        PlayerFollow();
    }

    private void PlayerFollow()
    {
        if (PlayerController.Instance.transform.eulerAngles.y > 170)
        {
            followOffsetZ = 0.85f;
        }
        else
        {
            followOffsetZ = -0.85f;
        }
        transform.position = Vector3.Lerp(transform.position,
            PlayerController.Instance.transform.position + (Vector3.forward * followOffsetZ),
            followSpeed * Time.deltaTime);
        transform.LookAt(PlayerController.Instance.transform);
        print(followOffsetZ);
    }
}
