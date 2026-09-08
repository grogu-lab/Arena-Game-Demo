using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;
    public bool updateRotation;
    [SerializeField] private float mouseSens = 0.1f;
    private float yaw;

    private void Awake()
    {
        Instance = this;
        updateRotation = true;
    }
    private void Update()
    {
        if (!updateRotation) return;
        RotateCamera();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void RotateCamera()
    {
        Vector2 mouseDel = Mouse.current.delta.ReadValue();

        yaw += mouseDel.x * mouseSens;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
