using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem.Interactions;

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
    private InputAction hotbarSlotSelect;
    private InputAction dropSelectedItem;
    public InteractIndicator indicator; 

    private int hotbarIndex = 0;
    public float equippedOpacity = 0.9f;
    public float normalOpacity = 0.60392f;


    

    private void OnEnable()
    {
        controls.FindActionMap("Player").Enable();
        hotbarSlotSelect.performed += SelectSlot;
        dropSelectedItem.performed += HandleDropItem;
        
    }

    private void OnDisable()
    {
        controls.FindActionMap("Player").Disable();
        hotbarSlotSelect.performed -= SelectSlot;
        dropSelectedItem.performed -= HandleDropItem;
    }

    private void Awake()
    {
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slots>());
        hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slots>());

        allSlots.AddRange(hotbarSlots);
        allSlots.AddRange(inventorySlots);

        interactControl = InputSystem.actions.FindAction("Interact");
        inventoryDisplay = InputSystem.actions.FindAction("Display Inventory");
        hotbarSlotSelect = InputSystem.actions.FindAction("Select Hotbar");
        dropSelectedItem = InputSystem.actions.FindAction("Drop");

        container.SetActive(false);
    }


    private void Update()
    {

        if (inventoryDisplay.WasPressedThisFrame())
        {
            container.SetActive(!container.activeInHierarchy);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }

        Pickup();

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

    public void Pickup()
    {
        if (indicator.currentItem != null && interactControl.WasPressedThisFrame())
        {
            AddItem(indicator.currentItem.weapon, indicator.currentItem.amount);
            Destroy(indicator.currentItem.gameObject);
            indicator.ClearIndicator();
            
        }

    }

    private void SelectSlot(InputAction.CallbackContext context)
    {
        if (int.TryParse(context.control.name, out int keyNumber))
        {
            hotbarIndex = keyNumber - 1;
            UpdateHotbarOpacity();
        }

    }

    private void UpdateHotbarOpacity()
    {
        for (int i=0; i < hotbarSlots.Count; i++)
        {
            Image icon = hotbarSlots[i].GetComponent<Image>();
            if (icon != null)
            {
                icon.color = (i == hotbarIndex)? new Color(0f, 0.1372549f, 0.6901961f, equippedOpacity): new Color(0f, 0.1372549f, 0.6901961f, normalOpacity);
            }
        }
    }

    private void HandleDropItem(InputAction.CallbackContext context)
    {
        Slots equippedSlot = hotbarSlots[hotbarIndex]; 

        if (!equippedSlot.HasItem()) return;
        WeaponData weaponItem = equippedSlot.GetItem();
        GameObject prefab = weaponItem.itemPrefab;

        if (prefab == null) return;
        GameObject droppedItem = Instantiate(prefab, Camera.main.transform.position + Camera.main.transform.forward, Quaternion.identity);
        PickupItem item = droppedItem.GetComponent<PickupItem>();

        item.weapon = weaponItem;
        item.amount = equippedSlot.GetAmount();

        equippedSlot.ClearSlot();
    }

    


}