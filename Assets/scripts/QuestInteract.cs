using UnityEngine;

public class QuestInteract : MonoBehaviour
{
    [SerializeField] private CanvasGroup questCanvas;
    public bool IsTriggered {get; private set;}

    private void Awake()
    {
        questCanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            questCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        questCanvas.alpha = 0f;
    }
}
