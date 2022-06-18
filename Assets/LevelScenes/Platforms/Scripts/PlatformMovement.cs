using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float position;
    [SerializeField] private float duration;
    [SerializeField] private float rebounceFactor = 1.5f;

    public void MovePlatform()
    {
        DOTween.Sequence().Append(transform.DOMoveY(position + rebounceFactor, duration / 2).SetEase(Ease.InQuad))
            .Append(transform.DOMoveY(position, duration / 2).SetEase(Ease.InSine));
        gameObject.layer = 7;
    }
}
