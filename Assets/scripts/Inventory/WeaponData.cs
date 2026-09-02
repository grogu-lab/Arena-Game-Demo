using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Item")]
public class WeaponData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public int maxStackSize;

    

    
}
