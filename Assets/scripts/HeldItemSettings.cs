using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItemSettings : MonoBehaviour
{
    public static HeldItemSettings HeldItemInstance;

    public InputActionAsset WeaponInputs;
    private InputAction attack;

    [SerializeField] public Vector3 itemRotation;
    [SerializeField] public Vector3 itemHoldPosition;

    [SerializeField] public int damage = 10;
    public bool attackPressed;

    private void OnEnable()
    {
        WeaponInputs.FindActionMap("Player").Enable();
        attack.performed += SwingWeapon;
    }

    private void OnDisable()
    {
        WeaponInputs.FindActionMap("Player").Disable();
        attack.performed -= SwingWeapon;
    }

    private void Awake()
    {
        HeldItemInstance = this;
        attackPressed = false;
        attack = InputSystem.actions.FindAction("Attack");
    }

    private void SwingWeapon(InputAction.CallbackContext context)
    {
        
    }
}
