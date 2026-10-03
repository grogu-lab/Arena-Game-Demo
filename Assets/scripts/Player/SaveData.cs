using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;
    private string savePath;

    public Vector3 position;
    public List<Slots> inventory;

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
        savePath = Path.Combine(Application.persistentDataPath, "Saves", "PositionSave.sav");
        saveKey = InputSystem.actions.FindAction("Save");
        LoadGame();
    }
    
    private void SaveGameData(InputAction.CallbackContext context)
    {
        SaveData data = new SaveData
        {
            position = MoveCharacter.PlayerInstance.Save().savePosition,
            inventory = Inventory.InventoryInstance.SlotSave().saveSlots
        };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadGame()
    {
        if (!Directory.Exists(Path.GetDirectoryName(savePath))) return;
        string jsonPos = File.ReadAllText(savePath);
        MoveCharacter.SavePlayerSettings loadPosition = JsonUtility.FromJson<MoveCharacter.SavePlayerSettings>(jsonPos);
        MoveCharacter.PlayerInstance.Load(loadPosition);

    }
}
