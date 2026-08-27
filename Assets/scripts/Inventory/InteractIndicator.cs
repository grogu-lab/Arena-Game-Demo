using System.ComponentModel;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private GameObject container;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PickupItem>(out var pickup))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            container.SetActive(!container.activeInHierarchy);
        }
    }
}
