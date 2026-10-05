using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class WeaponAnimations : MonoBehaviour
{
    private static readonly int ThrowTrigHash = Animator.StringToHash("ThrowTrig");
    private static readonly int SwingTrigHash = Animator.StringToHash("SwingTrig");
    public InputActionAsset WeaponControls;
   private Animator weaponAnimator;

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

      weaponAnimator = GetComponent<Animator>();
   }

   private void SwingWeapon(InputAction.CallbackContext context)
   {
      if(weaponAnimator == null) return;
      if(!gameObject.GetComponentInChildren<HeldItemSettings>()) return;
      weaponAnimator.SetTrigger(SwingTrigHash);
      flag = true;
   }

   private void ThrowWeapon(InputAction.CallbackContext context)
   {
      if(weaponAnimator == null) return;
      weaponAnimator.SetTrigger(ThrowTrigHash);
   }
}
