using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class QuestInteract : MonoBehaviour
{
    public static QuestInteract Instance;
    public CanvasGroup questButton;
    public CanvasGroup ongoingQuestButton;
    public bool questStarted = false;

    private ShowHideCanvasGroups displayHideCanvas;

    private void Awake()
    {
        displayHideCanvas = ShowHideCanvasGroups.Instance;
        Instance = this;
        displayHideCanvas.DisplayCanvasGroup(ongoingQuestButton, false);
        displayHideCanvas.DisplayCanvasGroup(questButton, false);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (!questStarted)
            {
                displayHideCanvas.DisplayCanvasGroup(questButton, true);
            }
            if (questStarted)
            {
                displayHideCanvas.DisplayCanvasGroup(ongoingQuestButton, true);
            }
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        displayHideCanvas.DisplayCanvasGroup(questButton, false);
        displayHideCanvas.DisplayCanvasGroup(ongoingQuestButton, false);
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
    }

    public void QuestStarted()
    {
        questStarted = true;
        displayHideCanvas.DisplayCanvasGroup(questButton, false);
        displayHideCanvas.DisplayCanvasGroup(ongoingQuestButton, true);
    }

}
