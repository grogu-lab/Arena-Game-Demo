using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCharacter : MonoBehaviour
{
    public InputActionAsset ActionInput;

    private InputAction jumpAction;
    private InputAction moveAction;
    

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;
    
    private Rigidbody rb;
    private Vector3 moveAmt;
    private bool isGrounded;

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
        moveAmt = moveAction.ReadValue<Vector2>();
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }
        

    }
    // procedures for movement and general mechanics
    
    
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveAmt.x, 0f, moveAmt.y);
        rb.MovePosition(rb.position + move * Time.deltaTime * moveSpeed);
    }
    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
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


    

}
