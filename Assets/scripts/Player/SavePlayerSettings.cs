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
