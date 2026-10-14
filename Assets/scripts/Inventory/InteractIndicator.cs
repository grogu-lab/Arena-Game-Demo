using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractIndicator : MonoBehaviour
{
    public static InteractIndicator Instance;
    [SerializeField] private TextMeshProUGUI promptText;
    public PickupItem currentItem;
    public float itemHeight;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {   
            if (other.TryGetComponent<PickupItem>(out var pickup))
            {
                currentItem = pickup;
                itemHeight = currentItem.transform.position.y;
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
        currentItem = null;
    }

    public void DisplayText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    private void BaseInteract()
    {
        
    }

    protected virtual void Interact()
    {
        
    }
}
