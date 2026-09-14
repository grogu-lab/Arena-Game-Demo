using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    [Header("Player")]
    public int healthBar = 50;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;
    
    [Header("Transform")]
    public Transform orientation;
    public Transform modelRotation;

    public bool updateRotation;

    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        Instance = this;
        updateRotation = true;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!updateRotation) return;
        Vector2 mouseDel = Mouse.current.delta.ReadValue();

        xRotation -= mouseDel.y * sensX;
        yRotation += mouseDel.x * sensY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        modelRotation.rotation = orientation.rotation;
    }

    private int DamageTaken()
    {
        return healthBar;
    }
}
