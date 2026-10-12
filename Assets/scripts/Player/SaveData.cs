using System.Data.Common;
using System.IO;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;
    private InputAction testLoadKey;
    private string saveFile;

    public bool saved = false;

    private void OnEnable()
    {
        saveControls.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        saveControls.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/inventory.json";
        saveKey = InputSystem.actions.FindAction("Save");
        testLoadKey = InputSystem.actions.FindAction("Load");

    }
    private void Update()
    {
        if (saveKey.WasPressedThisFrame())
        {
            SaveInventory();
        }
        if (testLoadKey.WasPressedThisFrame())
        {
            LoadInventory();
        }
    }

    public void SaveInventory()
    {
        SaveInventoryData data = new();
        foreach(Slots slot in Inventory.InventoryInstance.GetAllSlots())
        {
            SlotData slotData = new();
            if (slot.HasItem())
            {
                slotData.weaponID = slot.GetItem().itemID;
                slotData.amount = slot.GetAmount();
            }
            else
            {
                slotData.weaponID = "";
                slotData.amount = 0;
            }
            data.slotsForSave.Add(slotData);
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFile, json);
        Debug.Log($"Saved to: {saveFile}");


    }

    public void LoadInventory()
    {
        if(!File.Exists(saveFile)) return;
        string json = File.ReadAllText(saveFile);
        SaveInventoryData data = JsonUtility.FromJson<SaveInventoryData>(json);

        var slots = Inventory.InventoryInstance.GetAllSlots();
        for(int i = 0; i < slots.Count; i++)
        {
            if(i >= slots.Count) break;
            SlotData slotData = data.slotsForSave[i];

            if (!string.IsNullOrEmpty(slotData.weaponID))
            {
                WeaponData weaponItem = ItemDatabase.Instance.GetWeapon(slotData.weaponID);
                slots[i].SetItem(weaponItem, slotData.amount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("Load success");
    }

}

