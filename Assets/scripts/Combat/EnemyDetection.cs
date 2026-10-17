using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float detectDist = 8f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        DetectEnemy();
    }

    private void DetectEnemy()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(cam.transform.position, ray.direction * detectDist);
        RaycastHit detectHit;
        if(Physics.Raycast(ray, out detectHit, detectDist, mask))
        {
            transform.position = rb.position + Vector3.forward * 10f;
        }
    }
}
