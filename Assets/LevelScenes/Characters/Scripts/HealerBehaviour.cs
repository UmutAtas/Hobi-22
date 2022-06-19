using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject desiredPos;
    [SerializeField] private float time;
    [SerializeField] private Animator healerAnim;
    public bool parent = false;
    
    private void Protect()
    {
        parent = true;
        transform.SetParent(null);
        healerAnim.SetBool("CanWalk" , true);
        transform.DOMove(desiredPos.transform.position, time).OnComplete(() =>
        {
            healerAnim.SetBool("CanWalk", false);
        });
        transform.DORotate(new Vector3(0f, 180, 0f), time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wolf"))
        {
            Protect();
        }
    }
}
