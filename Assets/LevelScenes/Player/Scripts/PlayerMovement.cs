using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private float canSwipeRadius = 3000f;
    private float speed;
    private float rotationSpeed;
    private RaycastManager _raycastManager;
    
    private void Awake()
    {
        speed = PlayerController.Instance.playerMovementSpeed;
        rotationSpeed = PlayerController.Instance.playerRotationSpeed;
        _raycastManager = GetComponent<RaycastManager>();
    }

    public enum SwipeDirection
    {
        right,
        left,
        forward,
        backward,
    }
    
    public SwipeDirection currentDirection;

    public void MovePlayer()
    {
        var fingers = LeanTouch.Fingers;

         if (fingers.Count > 0)
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
                 _raycastManager.RayCaster();
                 GoToRaycastHit();
             }
             else if (fingers[0].Up)
             {
                 
             }
         }
    }
    
    private void CurrentSwipe()
    {
        switch (currentDirection)
        {
            case SwipeDirection.right:
                PlayerController.Instance.RayDirection = Vector3.right;
                break;
            
            case SwipeDirection.left:
                PlayerController.Instance.RayDirection = Vector3.left;
                break;
            
            case SwipeDirection.forward:
                PlayerController.Instance.RayDirection = Vector3.forward;
                break;
            
            case SwipeDirection.backward:
                PlayerController.Instance.RayDirection = Vector3.back;
                break;
        }
    }

    private Vector3 wallHitPoint;
    
    public void RotatePlayer()
    {
        var targetRotation = Quaternion.LookRotation(wallHitPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
    
    private void GoToRaycastHit()
    {
        wallHitPoint = _raycastManager.rayHitPoint;
        var endPosition = new Vector3(wallHitPoint.x, transform.position.y, wallHitPoint.z);
        var moveDuration =Vector3.Distance(transform.position ,wallHitPoint) / speed;
        transform.DOMove(endPosition,moveDuration).OnComplete((() =>
        {
            PlayerController.Instance.canSwipe = true;
        }));
    }

  
}
