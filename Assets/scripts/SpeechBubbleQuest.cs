using UnityEngine;

public class SpeechBubbleQuest : MonoBehaviour
{
    public void DisplayCanvasGroup(CanvasGroup canvas, bool visible)
    {
        canvas.alpha = visible ? 1f : 0f;
        canvas.blocksRaycasts = visible;
        canvas.interactable = visible;
    }
}
