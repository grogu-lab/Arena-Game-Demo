using System.ComponentModel;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private GameObject container;
    public PickupItem currentItem;
    
    private void Awake()
    {
        container.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {   
            container.SetActive(true);
            if (other.TryGetComponent<PickupItem>(out var pickup))
            {
                currentItem = pickup;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Item") && other.gameObject == currentItem?.gameObject)
        {
            ClearIndicator();
        }
    }

    public void ClearIndicator()
    {
        container.SetActive(false);
        currentItem = null;
    }
}
