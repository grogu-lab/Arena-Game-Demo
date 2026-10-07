using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Item")]
public class WeaponData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public GameObject heldItem;
    public int maxStackSize;
    public int dealsDamage;

    
}
