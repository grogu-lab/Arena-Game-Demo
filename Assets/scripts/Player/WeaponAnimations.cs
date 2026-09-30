using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAnimations : MonoBehaviour
{
   public InputActionAsset WeaponControls;
   public Animator weaponAnimator;

   private InputAction swingAttack;
   private InputAction throwAttack;

   public bool flag = false;

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
      if(weaponAnimator == null) return;
      if(!gameObject.GetComponentInChildren<HeldItemSettings>()) return;
      weaponAnimator.SetTrigger("SwingTrig");
      flag = true;
   }

   private void ThrowWeapon(InputAction.CallbackContext context)
   {
      if(weaponAnimator == null) return;
   }
}
