using UnityEngine;
using UnityEngine.InputSystem;


public class MoveCharacter : MonoBehaviour
{
    public InputActionAsset ActionInput;
    private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction attackAction;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;

    public Rigidbody rb;
    public GameObject heldItem;
    private Vector3 moveAmt;
    private Animation animator;
    private string currentAnimation = "";
    private bool isGrounded;

    private void OnEnable()
    {
        ActionInput.FindActionMap("Player").Enable();
        attackAction.performed += AttackPerformed;
    }

    private void OnDisable()
    {
        ActionInput.FindActionMap("Player").Disable();
        attackAction.performed -= AttackPerformed;
    }

    private void Awake()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponent<Animation>();

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
        rb.MovePosition(rb.position + moveAmt.y * transform.forward + moveAmt.x * moveSpeed * Time.deltaTime * transform.right);
        
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

    private void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(currentAnimation, crossfade);
        }
    }

    private void AttackPerformed(InputAction.CallbackContext context)
    {
      if(heldItem.GetComponent<HeldItemSettings>() == null) return;
      if (isGrounded)
        {
            ChangeAnimation("Attack");
        }
    }

}
