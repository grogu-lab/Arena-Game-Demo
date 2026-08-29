using System.ComponentModel;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private GameObject container;
    public bool inVicinity = false;
    
    private void Awake()
    {
        container.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            container.SetActive(true);
            inVicinity = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            container.SetActive(false);
            inVicinity = false;
        }
    }
}
