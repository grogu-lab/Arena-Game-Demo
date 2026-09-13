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

    private void OnEnable()
    {
        WeaponInputs.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        WeaponInputs.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        HeldItemInstance = this;
        attack = InputSystem.actions.FindAction("Attack");
    }

    private void SwingWeapon()
    {
        
    }
}
