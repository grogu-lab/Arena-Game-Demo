using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
   
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
