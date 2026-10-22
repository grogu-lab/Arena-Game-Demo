using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class QuestInteract : MonoBehaviour
{
    public static QuestInteract Instance;
    public CanvasGroup questButton;

    private void Awake()
    {
        Instance = this;
        ShowHideCanvasGroups.Instance.DisplayCanvasGroup(questButton, false);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ShowHideCanvasGroups.Instance.DisplayCanvasGroup(questButton, true);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ShowHideCanvasGroups.Instance.DisplayCanvasGroup(questButton, false);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }

}
