using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WitcherBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject desiredPos;
    [SerializeField] private Animator witchAnim;
    [SerializeField] private float time;
    public bool parent = false;
    [SerializeField] private GameObject portalEffect;
    
    private void Protect()
    {
        parent = true;
        transform.SetParent(null);
        transform.DOMove(desiredPos.transform.position, time).OnComplete(() =>
        {
            witchAnim.SetBool("CanAttack" , true);
            portalEffect.SetActive(true);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            Protect();
            StartCoroutine(ClosePortal(other));
        }
    }

    private IEnumerator ClosePortal(Collider other)
    {
        yield return new WaitForSeconds(1.5f);
        other.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
