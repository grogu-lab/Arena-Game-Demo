using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCharacter : MonoBehaviour
{
    public InputActionAsset ActionInput;

    InputAction jumpAction;

    [SerializeField] private float walkSpeed = 5f;
    private Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
