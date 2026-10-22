using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup questSpeechText;

    private void Start()
    {
        ShowHideCanvasGroups.Instance.DisplayCanvasGroup(questSpeechText, false);
    }

    public void StartQuestSpeech()
    {
        ShowHideCanvasGroups.Instance.DisplayCanvasGroup(questSpeechText, true);
    }

}
