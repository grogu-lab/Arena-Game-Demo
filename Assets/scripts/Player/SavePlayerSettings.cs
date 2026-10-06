using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayerPosition
{
    public Vector3 savePosition;
}

[System.Serializable]
public class SaveInventorySlots
{
    public List<Slots> saveSlots = new();
}

[System.Serializable]
public class SlotData
{
    public string weaponItemID;
    public int weaponAmount;
}

public class SavePlayerSettings : MonoBehaviour
{
    public SavePlayerSettings SaveInstance;
    private Dictionary<string, WeaponData> weaponLookup = new();


    private void Awake()
    {
        SaveInstance = this;
    }


}
