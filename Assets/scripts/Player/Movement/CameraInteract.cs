using System.Runtime.InteropServices;
using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    public static CameraInteract Instance;
    private Camera cam;
    [SerializeField] private float sightDist = 3f;
    [SerializeField] private LayerMask mask;
    public PickupItem item;
    public float itemHeight;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        InteractIndicator.Instance.DisplayText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit rayInfo;
        if(Physics.Raycast(ray, out rayInfo, sightDist, mask))
        {
            if(rayInfo.collider.GetComponent<PickupItem>() != null)
            {
                InteractIndicator.Instance.DisplayText(rayInfo.collider.GetComponent<PickupItem>().promptText);
            }
            if(rayInfo.collider.TryGetComponent<PickupItem>(out var pickup))
            {
                item = pickup;
                itemHeight = item.transform.position.y;
            }
        }
        
    }

    public void ClearIndicator()
    {
        item = null;
    }
}
