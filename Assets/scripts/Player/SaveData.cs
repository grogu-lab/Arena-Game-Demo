using System.IO;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;
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
        SaveVariables data = new SaveVariables
        {
            position = MoveCharacter.PlayerInstance.SavePosition(),
            inventory = SaveInventory()

        };

        string json = JsonUtility.ToJson(data, true);
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

    private void LoadGame()
    {
        if (!File.Exists(saveFile)) return;

        string json = File.ReadAllText(saveFile);
        SaveVariables data = JsonUtility.FromJson<SaveVariables>(json);

        MoveCharacter.PlayerInstance.LoadPosition(data.position);

        var slots = Inventory.InventoryInstance.GetAllSlots();
        for(int i = 0; i < slots.Count; i++)
        {
            if(i > data.inventory.slotsForSave.Count) break;

            SlotData dataSlot = data.inventory.slotsForSave[i];
            if (!string.IsNullOrEmpty(dataSlot.weaponID))
            {
                WeaponData outWeapon = ItemDatabase.Instance.GetWeapon(dataSlot.weaponID);
                slots[i].SetItem(outWeapon, dataSlot.amount);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }

    public SaveInventoryData SaveInventory()
    {
        SaveInventoryData data = new();
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
            data.slotsForSave.Add(dataSlot);
        }
        return data;
    }


}

[System.Serializable]
public class SaveVariables
{
    public SavePlayerPosition position;
    public SaveInventoryData inventory;
}
