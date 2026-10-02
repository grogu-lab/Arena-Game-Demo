using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;

    private string savePath = Path.Combine(Application.persistentDataPath, "Saves", "PositionSave.sav");

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
        saveKey = InputSystem.actions.FindAction("Save");
    }

    private void SavePlayerData(InputAction.CallbackContext context)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        string positionJson = JsonUtility.ToJson(MoveCharacter.PlayerInstance.Save());
        File.WriteAllText(savePath, positionJson);
    }
}
