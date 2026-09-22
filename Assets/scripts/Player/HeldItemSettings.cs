using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItemSettings : MonoBehaviour
{
    public static HeldItemSettings HeldItemInstance;
    public InputActionAsset actionMap;
    public GameObject heldItemPrefab;
    private InputAction throwAction;

    [SerializeField] public Vector3 itemRotation;
    [SerializeField] public Vector3 itemHoldPosition;
    [SerializeField] public int damage = 10;


    private void OnEnable()
    {
        actionMap.FindActionMap("Player").Enable();
        throwAction.performed += ThrowWeaponItem;
    }
    
    private void OnDisable()
    {
        actionMap.FindActionMap("Player").Disable();
        throwAction.performed -= ThrowWeaponItem;
    }

    private void Awake()
    {
        HeldItemInstance = this;
        throwAction = InputSystem.actions.FindAction("Throw");
    }

    private void ThrowWeaponItem(InputAction.CallbackContext context)
    {
        if(!gameObject.CompareTag("AllRange") || !gameObject.CompareTag("LongRange")) return;
        GameObject heldItem = Instantiate(heldItemPrefab, Inventory.InstantiateInventory.hand);
        
    }

}
