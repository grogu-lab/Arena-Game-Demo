using UnityEngine;

public class WeaponIdle : MonoBehaviour
{

    [SerializeField] private float rotationIdle;
    private void Update()
    {
        transform.Rotate(0f, 0f, rotationIdle);
    }
}
