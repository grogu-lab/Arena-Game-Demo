using UnityEngine;

public class HeldItemSettings : MonoBehaviour
{
    [SerializeField] private Vector3 itemRotation;
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(itemRotation);
    }
}
