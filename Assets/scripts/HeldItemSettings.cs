using UnityEngine;

public class HeldItemSettings : MonoBehaviour
{
    public static HeldItemSettings HeldItemInstance;
    [SerializeField] public Vector3 itemRotation;
    [SerializeField] public Vector3 itemHoldPosition;

    [SerializeField] public int damage;

    private void Awake()
    {
        HeldItemInstance = this;
    }
}
