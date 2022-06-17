using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [NonSerialized] public Vector3 rayDirection;
    private float canSwipeRadius = 3000f;
    
    public enum SwipeDirection
    {
        right,
        left,
        forward,
        backward,
    }
    
    public SwipeDirection currentDirection;
    
    public void CurrentSwipe()
    {
        switch (currentDirection)
        {
            case SwipeDirection.right:
                rayDirection = Vector3.right;
                break;
            
            case SwipeDirection.left:
                rayDirection = Vector3.left;
                break;
            
            case SwipeDirection.forward:
                rayDirection = Vector3.forward;
                break;
            
            case SwipeDirection.backward:
                rayDirection = Vector3.back;
                break;
        }
    }
    
    private void MovePlayer()
    {
        var fingers = LeanTouch.Fingers;

         if (fingers.Count> 0)
         { 
             if (fingers[0].Down)
             {
                    
             }
             else if (fingers[0].Set)
             { 
                 if (!PlayerController.Instance.canSwipe || fingers[0].SwipeScreenDelta.sqrMagnitude < canSwipeRadius)
                 {
                     return;
                 }
                
                 if (fingers[0].SwipeScreenDelta.x > 0 && Mathf.Abs(fingers[0].SwipeScreenDelta.x) 
                     > Mathf.Abs(fingers[0].SwipeScreenDelta.y))
                 {
                     currentDirection = SwipeDirection.right;
                     PlayerController.Instance.canSwipe = false;
                 }
                 else if (fingers[0].SwipeScreenDelta.x < 0 && Mathf.Abs(fingers[0].SwipeScreenDelta.x) 
                     > Mathf.Abs(fingers[0].SwipeScreenDelta.y))
                 {
                     currentDirection = SwipeDirection.left;
                     PlayerController.Instance.canSwipe = false;
                 }
                 else if (fingers[0].SwipeScreenDelta.y < 0 && Mathf.Abs(fingers[0].SwipeScreenDelta.x) 
                     < Mathf.Abs(fingers[0].SwipeScreenDelta.y))
                 {
                     currentDirection = SwipeDirection.backward;
                     PlayerController.Instance.canSwipe = false;
                 }
                 else if (fingers[0].SwipeScreenDelta.y > 0 && Mathf.Abs(fingers[0].SwipeScreenDelta.x) 
                     < Mathf.Abs(fingers[0].SwipeScreenDelta.y))
                 {
                     currentDirection = SwipeDirection.forward;
                     PlayerController.Instance.canSwipe = false;
                 }
                 CurrentSwipe();
               
                 GoToRaycastHit();
             }
             else if (fingers[0].Up)
             {
                 
             }
         }
    }
    
    private void GoToRaycastHit()
    {
        var wallHitPoint = _wallLocater.rayHitPoint;
        var endPosition = new Vector3(wallHitPoint.x, playerTransformHeight, wallHitPoint.z);
        var moveDuration =Vector3.Distance(transform.position ,wallHitPoint) * pTransformMoveSpeed;
        transform.DOMove(endPosition,moveDuration).SetEase(moveEase).OnComplete((() =>
        {
            //InputManager.I.canSwipe = true;
            canSwipe = true;
            //SoundManager.I.PlayOne("Test1");
            CameraShaker.Instance.ShakeOnce(camShakeMagnitude,camShakeRoughness,camShakeFadeIn,camShakeFadeout);
            LevelTilt.Instance.TiltRecovery();
        }));
        var endRotation = Vector3.up * rotateSpeed;
        transform.DORotate(endRotation, moveDuration, RotateMode.FastBeyond360);
    }
}
