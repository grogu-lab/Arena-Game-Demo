using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public GameObject inventorySlotParent;
    public GameObject hotbarObject;
    public GameObject container;

    private List<Slots> inventorySlots = new List<Slots>();
    private List<Slots> hotbarSlots = new List<Slots>();
    private List<Slots> allSlots = new List<Slots>();

    public InputActionAsset controls;
    private InputAction interactControl;
    private InputAction inventoryDisplay;

    public PickupItem itemPickup;
    public bool isTriggered = false;

    

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
    }


    private void Update()
    {

        if (inventoryDisplay.WasPressedThisFrame())
        {
            container.SetActive(!container.activeInHierarchy);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }

        if (isTriggered == true)
        {
            if (interactControl.WasPressedThisFrame())
            {
                AddItem(itemPickup.weapon, itemPickup.amount);
                Destroy(itemPickup.gameObject);
                isTriggered = false;
                itemPickup = null;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PickupItem>(out var pickup) == true)
        {
            itemPickup = pickup;
            isTriggered = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }


}