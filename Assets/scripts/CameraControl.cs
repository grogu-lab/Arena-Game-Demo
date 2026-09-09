using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

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
        if(!updateRotation) return;
        Vector2 mouseVal = Mouse.current.delta.ReadValue();
        

        yRotation += mouseVal.x * sensX;
        xRotation -= mouseVal.y * sensY;
        xRotation = Mathf.Clamp(xRotation, -25f, 25f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        modelRotation.rotation = orientation.rotation;
    }

}
