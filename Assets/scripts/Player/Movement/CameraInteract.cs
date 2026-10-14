using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float sightDist = 3f;
    [SerializeField] private LayerMask mask;
    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        InteractIndicator.Instance.DisplayText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * sightDist);
        RaycastHit rayInfo;
        if(Physics.Raycast(ray, out rayInfo, sightDist, mask))
        {
            if (rayInfo.collider.GetComponent<Interactables>() != null)
            {
                InteractIndicator.Instance.DisplayText(rayInfo.collider.GetComponent<Interactables>().promptText);   
            }
        }
        
    }
}
