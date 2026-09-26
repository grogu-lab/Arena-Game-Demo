using UnityEngine;

public class ArenaExit : MonoBehaviour
{
    [SerializeField] private Transform returnPos;
    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        other.transform.SetPositionAndRotation(returnPos.position, returnPos.rotation);
    }
}
