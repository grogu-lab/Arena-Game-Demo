using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    private int damage;
    private void Awake()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Weapon"))
        {
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void DamageTaken()
    {
        
    }
}
