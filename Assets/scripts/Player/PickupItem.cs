using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    public WeaponData weapon;
    public int amount = 0;
    public string promptText;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        
    }

}
