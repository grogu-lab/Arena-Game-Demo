using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestProgress : MonoBehaviour
{
    private List<Slots> playerInventory = new();
    [SerializeField] private WeaponData rewardApple;
    private int mushroomsRequired = 2;
    private int stonesRequired = 3;
    private bool stonesCollected = false;
    private bool mushroomsCollected = false;
    private bool questComplete = false;

    private void AcceptQuestItems()
    {
        if (QuestInteract.Instance.questStarted)
        {
            playerInventory = Inventory.InventoryInstance.GetAllSlots();
            foreach(Slots slot in playerInventory)
            {
                if (slot.HasItem())
                {
                    if(slot.GetItem().itemID == "Mushroom")
                    {
                        if(slot.GetAmount() >= mushroomsRequired)
                        {
                            slot.RemoveAmount(mushroomsRequired);
                            mushroomsCollected = true;
                        }
                        if(slot.GetAmount() < mushroomsRequired)
                        {
                            mushroomsRequired -= slot.GetAmount();
                            slot.RemoveAmount(slot.GetAmount());
                        }
                    }
                    if(slot.GetItem().itemID == "stone")
                    {
                        if(slot.GetAmount() >= stonesRequired)
                        {
                            slot.RemoveAmount(stonesRequired);
                            stonesCollected = true;
                        }
                        if(slot.GetAmount() < stonesRequired)
                        {
                            stonesRequired -= slot.GetAmount();
                            slot.RemoveAmount(slot.GetAmount());
                        }
                    }
                }
            }
            if(stonesCollected && mushroomsCollected)
            {
                questComplete = true;
            }
        }
    }

    private void GiveRewards()
    {
        if (questComplete)
        {
            Inventory.InventoryInstance.AddItem(rewardApple, 1);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            AcceptQuestItems();
            GiveRewards();
        }
    }
}
