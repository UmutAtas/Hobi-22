using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        CharacterRotation();
    }

    private void CharacterRotation()
    {
        targetRotation = Quaternion.LookRotation(PlayerController.Instance.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
