using UnityEngine;
using UnityEngine.InputSystem;
public class TriggerScript : MonoBehaviour
{   
    public InputActionAsset InputActions_TriggerScript;
    private InputAction attackAction;
    private Animator mainAnimator;

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

    private void Start()
    {
        mainAnimator = GetComponent<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
       if(mainAnimator == null) return;
       mainAnimator.SetTrigger("AttackOpen");
    }


}
