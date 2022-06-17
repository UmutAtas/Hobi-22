using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float followSpeed;

    private void Update()
    {
        PlayerFollow();
    }

    private void PlayerFollow()
    {
        transform.position = Vector3.Lerp(transform.position,
            PlayerController.Instance.transform.position + followOffset, followSpeed * Time.deltaTime);
        transform.LookAt(PlayerController.Instance.transform);
    }
}
