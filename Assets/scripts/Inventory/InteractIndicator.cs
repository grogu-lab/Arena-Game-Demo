using System.ComponentModel;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private GameObject container;
    
    private void Awake()
    {
        container.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            container.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("pickup"))
        {
            container.SetActive(false);
        }
    }
}
