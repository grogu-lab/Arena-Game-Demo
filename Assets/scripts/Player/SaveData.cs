using System.Data.Common;
using System.IO;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;
    private SaveInventoryData dataInv = new();
    private string saveFile;

    public bool saved = false;

    private void OnEnable()
    {
        saveControls.FindActionMap("Player").Enable();
        saveKey.performed += SaveGameData;
    }

    private void OnDisable()
    {
        saveControls.FindActionMap("Player").Disable();
        saveKey.performed -= SaveGameData;
    }

    private void Awake()
    {
        saveFile = Path.Combine(Application.persistentDataPath, "Saves", "GameSave.sav");
        saveKey = InputSystem.actions.FindAction("Save");

        LoadGame();
    }
    
    private void SaveGameData(InputAction.CallbackContext context)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(saveFile));
        SaveInventory();

        string json = JsonUtility.ToJson(dataInv, true);
        string tempFile = saveFile + ".txt";

        // Check if file already exists + write save data to a temp file
        File.WriteAllText(tempFile, json);

        if (File.Exists(saveFile)){
            File.Replace(tempFile, saveFile, null);
        }
        else{
            File.Move(tempFile, saveFile);
        }

        saved = true;
    }

    public void LoadGame()
    {
        if (!File.Exists(saveFile)) return;
        string json = File.ReadAllText(saveFile);
        SaveInventoryData data = JsonUtility.FromJson<SaveInventoryData>(json);
        LoadInventory();
  
        
    }

    public void SaveInventory()
    {
        foreach(Slots slot in Inventory.InventoryInstance.GetAllSlots())
        {
            SlotData dataSlot =  new();
            if (slot.HasItem())
            {
                dataSlot.weaponID = slot.GetItem().itemID;
                dataSlot.amount = slot.GetAmount();
            }
            else
            {
                dataSlot.weaponID = "";
                dataSlot.amount = 0;
            }
            dataInv.slotsForSave.Add(dataSlot);
        }
    }

    private void LoadInventory()
    {
        var slots = Inventory.InventoryInstance.GetAllSlots();
        for(int i = 0; i < slots.Count; i++)
        {
            if(i >= slots.Count) break;
            SlotData slot = dataInv.slotsForSave[i];

            if (!string.IsNullOrEmpty(slot.weaponID))
            {
                WeaponData item = ItemDatabase.Instance.GetWeapon(slot.weaponID);
                slots[i].SetItem(item, slot.amount);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }
    }


}

[System.Serializable]
public class SaveVariables
{
    public SavePlayerPosition position;
    public SaveInventoryData inventory;

    
    
}
