using UnityEngine;

public class ArenaExit : MonoBehaviour
{
    [SerializeField] private Transform returnPos;
    
    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Respawn")) return;
        gameObject.transform.SetPositionAndRotation(returnPos.position, returnPos.rotation);
    }


}
