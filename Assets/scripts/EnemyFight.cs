using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public GameObject playerCharacter;
    public EnemySO enemy;

    public int health;
    public bool isHit;
    private int damage;
    

    private void Awake()
    {
        health = 50;
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
                damage = weapon.damage;
                health -= damage;
                if (health <= 0)
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

