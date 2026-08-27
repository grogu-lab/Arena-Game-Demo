using System.ComponentModel;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private GameObject container;
    
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            container.SetActive(container.activeInHierarchy);
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
