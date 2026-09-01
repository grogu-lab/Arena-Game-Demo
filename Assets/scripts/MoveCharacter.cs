using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCharacter : MonoBehaviour
{
    public InputActionAsset ActionInput;

    private InputAction jumpAction;
    private InputAction moveAction;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;

    public Rigidbody rb;
    private Vector3 moveAmt;
    private bool isGrounded;
    public float mouseSensitivity = 0.14f;
    private float yaw;

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
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        moveAmt = moveAction.ReadValue<Vector2>();
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }
        Look();
        

    }
    // procedures for movement and general mechanics
    
    
    private void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + (transform.forward * moveAmt.y + transform.right * moveAmt.x) * Time.deltaTime * moveSpeed);
        
    }

    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, jumpSpeed, 0), Vector3.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Look()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * mouseSensitivity;
        
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        
    }
}
