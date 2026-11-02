using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestProgress : MonoBehaviour
{
    private List<Slots> playerInventory = new();
    private int amountOnHand;
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
                        int amountRequired = 2;
                        if(slot.GetAmount() < amountRequired)
                        {
                            amountRequired -= slot.GetAmount();
                            slot.RemoveAmount(slot.GetAmount());
                        
                        }
                        if(slot.GetAmount() >= amountRequired)
                        {
                            slot.RemoveAmount(amountRequired);

                        }
                    }
                    if(slot.GetItem().itemID == "stone")
                    {
                        int amountRequired = 3;
                        if(slot.GetAmount() < amountRequired)
                        {
                            amountRequired -= slot.GetAmount();
                            slot.RemoveAmount(slot.GetAmount());
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        
    }
}
