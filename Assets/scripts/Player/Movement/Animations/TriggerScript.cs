using UnityEngine;
using UnityEngine.InputSystem;
public class TriggerScript : MonoBehaviour
{   
    public InputActionAsset InputActions_TriggerScript;
    private InputAction attackAction;
    private Animator swingAnimator;


    private void OnEnable()
    {
        InputActions_TriggerScript.FindActionMap("Player").Enable();
        attackAction.performed += PerformAttack;
    }

    private void OnDisable()
    {
        InputActions_TriggerScript.FindActionMap("Player").Disable();
        attackAction.performed -= PerformAttack;
    }

    private void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Start()
    {
        swingAnimator = GetComponent<Animator>();
        
    }

    private void PerformAttack(InputAction.CallbackContext context)
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
