using UnityEngine;
using UnityEngine.UI;

public class QuestInteract : MonoBehaviour
{
    [SerializeField] private GameObject questButton;
    
    public bool IsTriggered {get; private set;}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            questButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            questButton.SetActive(false);
        }
    }

}
