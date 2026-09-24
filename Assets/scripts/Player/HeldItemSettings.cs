using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItemSettings : MonoBehaviour
{
    public static HeldItemSettings HeldItemInstance;
    public WeaponData handWeaponItem;
    public int damageDealt;
    public Vector3 itemHoldPosition;
    public Vector3 itemRotation;


    private void Awake()
    {
        HeldItemInstance = this;
        handWeaponItem = Inventory.InstantiateInventory.equipItem;
        damageDealt = handWeaponItem.dealsDamage;
    }


}
