using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveData : MonoBehaviour
{
    public InputActionAsset saveControls;
    private InputAction saveKey;

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
        MoveCharacter.PlayerInstance.Save();
    }
}
