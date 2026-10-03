using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;
    private string savePath;

    private void OnEnable()
    {
        saveControls.FindActionMap("Player").Enable();
        saveKey.performed += SavePlayerData;
    }

    private void OnDisable()
    {
        saveControls.FindActionMap("Player").Disable();
        saveKey.performed -= SavePlayerData;
    }

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Saves", "PositionSave.sav");
        saveKey = InputSystem.actions.FindAction("Save");
        LoadPlayerData();
    }

    private void SavePlayerData(InputAction.CallbackContext context)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        
        MoveCharacter.SavePlayerSettings savePlayerPosition = MoveCharacter.PlayerInstance.Save();
        string positionJson = JsonUtility.ToJson(savePlayerPosition);
        File.WriteAllText(savePath, positionJson);
    }

    private void LoadPlayerData()
    {
        if (!Directory.Exists(Path.GetDirectoryName(savePath))) return;
        string json = File.ReadAllText(savePath);
        MoveCharacter.SavePlayerSettings loadPosition = JsonUtility.FromJson<MoveCharacter.SavePlayerSettings>(json);
        MoveCharacter.PlayerInstance.Load(loadPosition);

    }
}
