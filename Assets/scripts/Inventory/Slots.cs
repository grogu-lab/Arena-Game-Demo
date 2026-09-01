using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;
    public InputActionAsset inputActions;
    private InputAction selectSlot;
    private WeaponData heldItem;
    private int itemQty;
    private Image iconImage;
    private TextMeshProUGUI qtyText;
    private Material highlightSlot;
    private Material originalHighlight;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        selectSlot.performed += SelectSlot;
        
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
        selectSlot.performed -= SelectSlot;
    }
    private void Awake()
    {
        iconImage = transform.GetChild(0).GetComponent<Image>();
        qtyText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        selectSlot = InputSystem.actions.FindAction("Select Hotbar");

        UpdateSlot();
        
    }

    public WeaponData GetItem()
    {
        return heldItem;
    }

    public int GetAmount()
    {
        return itemQty;
    }

    public void SetItem(WeaponData weapon, int amount)
    {
        heldItem = weapon;
        itemQty = amount;

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (heldItem != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = heldItem.icon;
            qtyText.text = itemQty.ToString();
        }

        if(heldItem == null)
        {
            iconImage.enabled = false;
            qtyText.text = "";
        }
    }

    public int AddAmount(int amountToAdd)
    {
        itemQty += amountToAdd;
        UpdateSlot();
        return itemQty;
    }

    public int RemoveAmount(int amountToRemove)
    {
        if (itemQty != 0)
        {
            itemQty -= amountToRemove;

            if (itemQty <= 0)
            {
                ClearSlot();
            }
            else
            {
                UpdateSlot();
            }
        }

        return itemQty;
    }

    public void ClearSlot()
    {
        heldItem = null;
        itemQty = 0;
        UpdateSlot();
    }

    public bool HasItem()
    {
        return heldItem != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    public void SelectSlot(InputAction.CallbackContext context)
    {
        // Automatically runs when any of the hotbar keys are pressed


    }
}
