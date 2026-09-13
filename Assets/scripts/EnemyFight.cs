using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public GameObject playerCharacter;
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
       TakeDamage();
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void TakeDamage()
    {
        if(enemy.enemyHealth <= 0) return;
        playerCharacter.TryGetComponent<HeldItemSettings>(out var weapon);
        if(weapon == null) return;
        damage = weapon.damage; // damage variable in this file assigned the value of the damage field from HeldItemSettings

        if (damage >= enemy.enemyHealth || enemy.enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            enemy.enemyHealth -= damage;
        }
    }
}
