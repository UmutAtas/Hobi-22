using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WarriorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject desiredPos;
    [SerializeField] private float time;
    [SerializeField] private Animator soldierAnim;
    public bool parent = false;
    
    private void Protect()
    {
        print("girdi");
        parent = true;
        transform.SetParent(null);
        soldierAnim.SetBool("CanWalk" , true);
        transform.DOMove(desiredPos.transform.position, time).OnComplete(() =>
        {
            soldierAnim.SetBool("CanWalk", false);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Protect();
        }
    }
}
