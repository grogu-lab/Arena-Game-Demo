using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItemSettings : MonoBehaviour
{
    public WeaponData handWeaponItem;
    public int damageDealt;

    private void Awake()
    {
        handWeaponItem = Inventory.InstantiateInventory.equipItem;
        damageDealt = handWeaponItem.dealsDamage;
    }


}
