using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInventoryData
{
    public List<SlotData> slotsForSave = new();
}

[System.Serializable]
public class SlotData
{
    public string weaponID;
    public int amount;
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    private Dictionary<string, WeaponData> weaponLookup = new();
    public List<WeaponData> allInvWeapons;

    private void Awake()
    {
        Instance = this;
        foreach(var weapon in allInvWeapons)
        {
            weaponLookup[weapon.itemID] = weapon;
        }
    }

    public WeaponData GetWeapon(string id)
    {
        return weaponLookup.ContainsKey(id) ? weaponLookup[id] : null;
    }

}
