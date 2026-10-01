using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public EnemySO enemy;

    public int health;
    public bool isHit;
    private int damage;
    private Rigidbody rbEnemy;
    

    private void Awake()
    {
        health = enemy.enemyHealth;
        rbEnemy = GetComponent<Rigidbody>();
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(health <= 0) return;
        isHit = true;
        if (other.CompareTag("FarRange") || other.CompareTag("CloseRange") || other.CompareTag("AllRange"))
        {
            if(other.TryGetComponent<HeldItemSettings>(out var weapon))
            {
                damage = weapon.damageDealt;
                health -= damage;
                rbEnemy.AddForce((-transform.forward * 700f) + (Vector3.up * 20f), ForceMode.Impulse);
                if(health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHit = false;
    }

}

