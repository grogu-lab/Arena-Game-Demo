using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItemSettings : MonoBehaviour
{
    public static HeldItemSettings HeldItemInstance;

    public GameObject heldItemPrefab;

    [SerializeField] public Vector3 itemRotation;
    [SerializeField] public Vector3 itemHoldPosition;
    [SerializeField] public int damage = 10;

    private void Awake()
    {
        HeldItemInstance = this;
    }

}
