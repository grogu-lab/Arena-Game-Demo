using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Item")]
public class WeaponData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject itemPrefab;
    public GameObject handItemPrefab;
    public int maxStackSize;

    
}
