using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCharacter : MonoBehaviour
{
    public GameObject groundSetting = null;
    public InputActionAsset ActionInput;
    private InputAction jumpAction;
    private InputAction moveAction;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;

    public Rigidbody rb;
    private Vector3 moveAmt;
    private bool isGrounded;

    private void OnEnable()
    {
        ActionInput.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        ActionInput.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

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
        
        rb.MovePosition(rb.position + (transform.forward * moveAmt.y + transform.right * moveAmt.x) * Time.deltaTime * moveSpeed);
        
    }

    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, jumpSpeed, 0), Vector3.up, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if(contact.normal.y > 0.5f)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
