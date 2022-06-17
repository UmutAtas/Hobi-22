using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
   [NonSerialized] public bool canSwipe;
   public float playerMovementSpeed;
   
   private Vector3 rayDirection;
   private PlayerMovement _playerMovement;

   private void Awake()
   {
      _playerMovement = GetComponentInChildren<PlayerMovement>();
      canSwipe = true;
   }

   public Vector3 RayDirection
   {
      get => rayDirection;
      set => rayDirection = value;
   }  

   private void FixedUpdate()
   {
       _playerMovement.MovePlayer();
   }
}
