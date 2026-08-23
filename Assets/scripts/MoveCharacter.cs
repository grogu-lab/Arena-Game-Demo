using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCharacter : MonoBehaviour
{
    public InputActionAsset ActionInput;

    private InputAction jumpAction;
    private InputAction moveAction;
    

    [SerializeField] private float walkSpeed = 5f;
    
    private Rigidbody rb;
    private Vector3 moveAmt;

    private void OnEnable()
    {
        ActionInput.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        ActionInput?.FindActionMap("Player")?.Disable();
    }

    private void Awake()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveAmt = moveAction.ReadValue<Vector3>();
        if (jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.down, ForceMode.Impulse);
    }

    

}
