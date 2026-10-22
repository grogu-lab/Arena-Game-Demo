using UnityEngine;

public class ShowHideCanvasGroups : MonoBehaviour
{
    public static ShowHideCanvasGroups Instance;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void DisplayCanvasGroup(CanvasGroup canvas, bool visible)
    {
        canvas.alpha = visible ? 1f : 0f;
        canvas.blocksRaycasts = visible;
        canvas.interactable = visible;
    }

    public void DisplayQuestText()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
}
