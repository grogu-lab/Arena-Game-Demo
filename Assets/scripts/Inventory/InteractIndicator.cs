using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractIndicator : MonoBehaviour
{
    public static InteractIndicator Instance;
    [SerializeField] private TextMeshProUGUI promptTextTM;
    public PickupItem currentItem;
    public float itemHeight;

    private void Awake()
    {
        Instance = this;
    }


    public void DisplayText(string promptMessage)
    {
        promptTextTM.text = promptMessage;
    }

}
