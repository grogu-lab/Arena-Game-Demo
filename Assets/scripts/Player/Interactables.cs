using UnityEngine;

using System.Diagnostics;

public class Interactables : PickupItem
{
    protected override void Interact()
    {
        UnityEngine.Debug.Log("interacted with" + gameObject.name);
    }
}
