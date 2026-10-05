using System.IO;
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
            inventory = Inventory.InventoryInstance.SlotSave()
        };

        string json = JsonUtility.ToJson(data, true);
        string tempFile = saveFile + ".txt";

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

    }

    [System.Serializable]
    public class SaveVariables
    {
        public SavePlayerPosition position;
        public SaveInventorySlots inventory;
    }

}
