using UnityEngine;

public class ArenaExit : MonoBehaviour
{
    public GameObject playerObject;
    private void OnTriggerEnter(Collider collider)
    {
        if(!collider.CompareTag("Player")) return;
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerObject.TryGetComponent<MoveCharacter>(out var player);
            player.rb.transform.position = player.returnPos;
        }
    }
}
