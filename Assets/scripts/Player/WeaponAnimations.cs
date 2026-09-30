using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAnimations : MonoBehaviour
{
   public InputActionAsset WeaponControls;
   public Animator weaponAnimator;

   private InputAction swingAttack;
   private InputAction throwAttack;

   private void OnEnable()
   {
      WeaponControls.FindActionMap("Player").Enable();
      swingAttack.performed += SwingWeapon;
      throwAttack.performed += ThrowWeapon;
   }

   private void OnDisable()
   {
      WeaponControls.FindActionMap("Player").Disable();
      swingAttack.performed -= SwingWeapon;
      throwAttack.performed -= ThrowWeapon;
   }

   private void Awake()
   {
      swingAttack = InputSystem.actions.FindAction("Attack");
      throwAttack = InputSystem.actions.FindAction("Throw");
   }

   private void SwingWeapon(InputAction.CallbackContext context)
   {
      
   }

   private void ThrowWeapon(InputAction.CallbackContext context)
   {
      
   }
}
