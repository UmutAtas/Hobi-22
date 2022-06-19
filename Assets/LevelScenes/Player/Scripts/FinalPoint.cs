using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : MonoBehaviour
{
    [SerializeField] private GameObject finalParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinalPoint"))
        {
           finalParticle.SetActive(true);
           StartCoroutine(Finish());
        }
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.Gamestate = GameManager.GAMESTATE.Finish;
    }
}
