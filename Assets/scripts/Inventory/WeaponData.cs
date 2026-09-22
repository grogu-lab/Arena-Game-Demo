using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Item")]
public class WeaponData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public GameObject heldItem;
    public int maxStackSize;
    public int dealsDamage;
    public Vector3 heldItemPosition;
    public Vector3 heldItemRotation;

    
}
