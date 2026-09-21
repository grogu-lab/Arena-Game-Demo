using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class TriggerScript : MonoBehaviour
{   
    public InputActionAsset InputActions_TriggerScript;
    private InputAction attackAction;
    private InputAction throwAction;
    private Animator swingAnimator;
    private GameObject throwableItem;


    private void OnEnable()
    {
        InputActions_TriggerScript.FindActionMap("Player").Enable();
        attackAction.performed += SwingAttack;
        throwAction.performed += ThrowAttack;
    }

    private void OnDisable()
    {
        InputActions_TriggerScript.FindActionMap("Player").Disable();
        attackAction.performed -= SwingAttack;
        throwAction.performed -= ThrowAttack;
    }

    private void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        throwAction = InputSystem.actions.FindAction("Throw");
    }

    private void Start()
    {
        swingAnimator = GetComponent<Animator>();
        
    }

    private void SwingAttack(InputAction.CallbackContext context)
    {
        if(HeldItemSettings.HeldItemInstance == null) return;
        if (HeldItemSettings.HeldItemInstance.gameObject.CompareTag("CloseRange") || HeldItemSettings.HeldItemInstance.gameObject.CompareTag("AllRange"))
        {
            swingAnimator.SetTrigger("TriOpen");
        }
        else
        {
            return;
        }
    }

    private void ThrowAttack(InputAction.CallbackContext context)
    {
        if(HeldItemSettings.HeldItemInstance == null || HeldItemSettings.HeldItemInstance.gameObject.CompareTag("CloseRange")) return;

    }

}
