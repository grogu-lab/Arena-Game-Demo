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

    private void Awake()
    {
        HeldItemInstance = this;
        attackPressed = false;
        attack = InputSystem.actions.FindAction("Attack");
    }

}
