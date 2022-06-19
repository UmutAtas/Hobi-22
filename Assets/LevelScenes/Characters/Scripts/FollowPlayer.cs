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

    [SerializeField] private Animator healerAnim;
    [SerializeField] private Animator soldierAnim;
    [SerializeField] private Animator witchAnim;
    [SerializeField] private WarriorBehaviour _warriorBehaviour;
    [SerializeField] private WitcherBehaviour _witcherBehaviour; 
    [SerializeField] private HealerBehaviour _healerBehaviour; 
    
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
            if (!_healerBehaviour.parent)
            {
                healerAnim.SetBool("CanWalk" , true);    
            }
            if (!_warriorBehaviour.parent)
            {
                soldierAnim.SetBool("CanWalk" , true);
            }
            if (!_witcherBehaviour.parent)
            {
                witchAnim.SetBool("CanWalk" , true);
            }
        }
        else
        {
            healerAnim.SetBool("CanWalk" , false);
            soldierAnim.SetBool("CanWalk" , false);
            witchAnim.SetBool("CanWalk" , false);
        }
    }
}
