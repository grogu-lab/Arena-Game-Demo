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
    public GameObject playerCharacter;

    private List<Slots> inventorySlots = new List<Slots>();
    private List<Slots> hotbarSlots = new List<Slots>();
    private List<Slots> allSlots = new List<Slots>();

    public Image dragIcon;

    public InteractIndicator indicator; 
    public InputActionAsset controls;
    private InputAction interactControl;
    private InputAction inventoryDisplay;
    private InputAction hotbarSlotSelect;
    private InputAction dropSelectedItem;
    private InputAction dragSlot;

    private Slots draggedSlot = null;
    private bool isDragging = false;
    

    private int hotbarIndex = 0;
    public float equippedOpacity = 0.9f;
    public float normalOpacity = 0.60392f;


    

    private void OnEnable()
    {
        controls.FindActionMap("Player").Enable();
        hotbarSlotSelect.performed += SelectSlot;
        dropSelectedItem.performed += HandleDropItem;
        dragSlot.performed += StartDrag;
        
    }

    private void OnDisable()
    {
        controls.FindActionMap("Player").Disable();
        hotbarSlotSelect.performed -= SelectSlot;
        dropSelectedItem.performed -= HandleDropItem;
        dragSlot.performed -= EndDrag;
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
        dragSlot = InputSystem.actions.FindAction("Drag");

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
            hotbarIndex = keyNumber -1;
            UpdateHotbarOpacity();

            Slots slot = hotbarSlots[hotbarIndex];
            if (slot.HasItem())
            {
                
            }
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
        GameObject droppedItem = Instantiate(prefab,playerCharacter.transform.position + playerCharacter.transform.forward, Quaternion.Euler(-89.98f, 0, 0));
        PickupItem item = droppedItem.GetComponent<PickupItem>();

        item.weapon = weaponItem;
        item.amount = equippedSlot.GetAmount();

        equippedSlot.ClearSlot();
    }

    private void StartDrag(InputAction.CallbackContext context)
    {
        Slots hovered = GetHoveredSlot();
        if (hovered != null && hovered.HasItem())
        {
            draggedSlot = hovered;
            isDragging = true;
            dragIcon.sprite = hovered.GetItem().icon;
            dragIcon.color = new Color(0f, 0.1372549f, 0.6901961f, normalOpacity);
            dragIcon.enabled = true;
        }
    }  

    private void EndDrag(InputAction.CallbackContext context)
    {
        Slots hovered = GetHoveredSlot();
        if (hovered != null)
        {
            HandleDropSlot(draggedSlot, hovered);
            dragIcon.enabled = false;
            draggedSlot = null;
            isDragging = false;
            
        }
    }

    private Slots GetHoveredSlot()
    {
        foreach(Slots s in allSlots)
        {
            if (s.hovering)
            {
                return s;
            }
        }
        return null;
    }

    private void HandleDropSlot(Slots from, Slots to)
    {
        // has to drop the item on the floor if dragged out of the inventory

        // isDragging > ClearSlot() > hovered.position = mousedrag.position
        // hovered = false > Instantiate(prefab, playerCharacter.transform.position + playerCharacter.transform.forward, Quaternion.Identity)

        if (from == to) return;

        // Stack items
        if (to.HasItem() && to.GetItem() == from)
        {

            int max = to.GetItem().maxStackSize;
            int space = max - to.GetAmount();

            if (space > 0)
            {
                int move = Mathf.Min(space, from.GetAmount()); // quantity of the item you can add to a slot
                to.SetItem(to.GetItem(), to.GetAmount() + move);
                from.SetItem(from.GetItem(), from.GetAmount() - move);

                if (from.GetAmount() <= 0)
                {
                    from.ClearSlot();
                }
                return;
            } 
        }
        // Swap items
        if(to.HasItem())
        {
            WeaponData tempItem = to.GetItem();
            int tempAmount = to.GetAmount();

            to.SetItem(from.GetItem(), from.GetAmount());
            from.SetItem(tempItem, tempAmount);
            return;
        }
        // Place in another (empty) slot

        if (!to.HasItem())
        {
            to.SetItem(from.GetItem(), from.GetAmount());
            from.ClearSlot();
            return;
        }
    }
}