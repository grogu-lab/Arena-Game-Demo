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

        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void TakeDamage()
    {
        HeldItemSettings weapon = handItem.GetComponent<HeldItemSettings>();
        damage = weapon.damage; // damage variable in this file assigned the value of the damage field from HeldItemSettings

        enemy.enemyHealth -= damage;

    }
}
