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
    public GameObject arenaObject;
    private Vector3 arenaRange;
    private Vector3 arenaCenter;
    private Vector3 moveAmt;
    public bool isGrounded;

    private void OnEnable()
    {
        ActionInput.FindActionMap("Player").Enable();
        jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        ActionInput.FindActionMap("Player").Disable();
        jumpAction.performed -= Jump;
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
    }
    // procedures for movement and general mechanics
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmt.y * transform.forward + moveAmt.x * moveSpeed * Time.deltaTime * transform.right);
    }

    private void Jump(InputAction.CallbackContext context)
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
