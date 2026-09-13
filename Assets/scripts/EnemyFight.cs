using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public GameObject handItem;
    public EnemySO enemy;
    private int damage;

    private void Awake()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Weapon")) return;
        else
        {
            TakeDamage();
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void TakeDamage()
    {
        if(enemy.enemyHealth <= 0) return;
        handItem.TryGetComponent<HeldItemSettings>(out var weapon);
        damage = weapon.damage; // damage variable in this file assigned the value of the damage field from HeldItemSettings

        if (damage >= enemy.enemyHealth)
        {
            Destroy(gameObject);
        }
        else
        {
            enemy.enemyHealth -= damage;
        }
        
        
        
    }
}
