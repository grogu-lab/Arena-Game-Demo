using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public WeaponData pickaxeItem;
    public GameObject inventorySlotParent;
    public GameObject hotbarObject;

    private List<Slots> inventorySlots = new List<Slots>();
    private List<Slots> hotbarSlots = new List<Slots>();
    private List<Slots> allSlots = new List<Slots>();

    public InputActionAsset controls;
    private InputAction interactControl;
    public InputAction inventoryDisplay;
    public bool isVisible = false;

    [SerializeField] private CanvasGroup panelCanvasGroup;
    
    



    private void OnEnable()
    {
        controls?.FindActionMap("Player")?.Enable();
        
    }

    private void OnDisable()
    {
        controls?.FindActionMap("Player")?.Disable();
    }

    private void Awake()
    {
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slots>());
        hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slots>());

        allSlots.AddRange(inventorySlots);
        allSlots.AddRange(hotbarSlots);

        interactControl = InputSystem.actions.FindAction("Interact");
        inventoryDisplay = InputSystem.actions.FindAction("Display Inventory");

        panelCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        panelCanvasGroup.alpha = 0f;
        panelCanvasGroup.interactable = isVisible;
        panelCanvasGroup.blocksRaycasts = isVisible;
    }

    private void Update()
    {

        if (interactControl.WasPressedThisFrame())
        {
            AddItem(pickaxeItem, 1);
        }

        if (inventoryDisplay.WasPressedThisFrame())
        {
            isVisible = true;
            ToggleInventory();
        }

    }

    public void AddItem(WeaponData weapon, int amount)
    {
        int remaining = amount;
        foreach (Slots slot in allSlots)
        {
            if(slot.HasItem() && slot.GetItem() == weapon)
            {
                int currentAmount = slot.GetAmount();
                int maxStackSize = weapon.maxStackSize;

                if (currentAmount < maxStackSize)
                {
                    int spaceLeft = maxStackSize - currentAmount;

                    // checks if there's more space than items or more items
                    // than the number of space available in the inventory

                    int amountToAdd = Mathf.Min(remaining, spaceLeft);

                    slot.SetItem(weapon, currentAmount + amountToAdd);
                    remaining -= amountToAdd;

                    if (remaining <= 0)
                    {
                        return;
                    }
                }
            }
        }

        foreach(Slots slot in allSlots)
        {
            if (!slot.HasItem())
            {
                int amountToPlace = Mathf.Min(weapon.maxStackSize, remaining);
                slot.SetItem(weapon, amountToPlace);
                remaining -= amountToPlace;

                if (remaining <= 0)
                {
                    return;
                }
            }
        }

        if (remaining > 0)
        {
            Debug.Log($"Inventory is full, {remaining} of {weapon.itemName}");
        }
    }

     public void ToggleInventory()
    {
        if (isVisible == true)
        {
            panelCanvasGroup.alpha = 1f;
            panelCanvasGroup.interactable = isVisible;
            panelCanvasGroup.blocksRaycasts = isVisible;
        }
    }

}