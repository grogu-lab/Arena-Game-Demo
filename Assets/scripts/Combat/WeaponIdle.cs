using UnityEngine;

public class WeaponIdle : MonoBehaviour
{

    [SerializeField] private float rotationIdle;
    [SerializeField] private float bopHeight = 0.25f;
    [SerializeField] private float bopSpeed = 1f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        transform.Rotate(0f, rotationIdle, 0f);
        float offset = Mathf.Sin(bopSpeed * Time.time) * bopHeight;
        transform.position = startPosition + new Vector3(0f, offset, 0f);
        
    }
}
