using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float detectDist = 7f;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private float enemySpeed = 5f;
    private Rigidbody rb;
    public bool testBool = false;

    public GameObject playerObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 playerPos = MoveCharacter.PlayerInstance.transform.position;
        if (PlayerDetected())
        {
            Vector3 toPlayer = playerPos - transform.position;
            Vector3 dir = toPlayer.normalized;

            rb.linearVelocity = new Vector3(dir.x * enemySpeed, rb.linearVelocity.y, dir.z * enemySpeed);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private bool PlayerDetected()
    {
        Vector3 eye = transform.position + Vector3.up * 0.5f;
        Vector3 player = MoveCharacter.PlayerInstance.transform.position + Vector3.up * 0.5f;
        Vector3 toPlayer = player - eye;

        Debug.DrawRay(eye, toPlayer, Color.red);

        if(toPlayer.sqrMagnitude > detectDist * detectDist) return false;
        
        Vector3 flat = toPlayer;
        flat.y = 0f;

        if(Vector3.Angle(transform.forward, flat) > fieldOfView * 0.5f) return false;

        if(Physics.Raycast(eye, toPlayer.normalized, out RaycastHit hit, detectDist))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 left = Quaternion.Euler(0f, -fieldOfView * 0.5f, 0f) * transform.forward;
        Vector3 right = Quaternion.Euler(0f, fieldOfView * 0.5f, 0f) * transform.forward;
        Gizmos.DrawRay(transform.position, left * detectDist);
        Gizmos.DrawRay(transform.position, right * detectDist);
    }
}
