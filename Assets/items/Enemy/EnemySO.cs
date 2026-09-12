using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/Enemy")]
public class EnemySO : ScriptableObject
{
    public int enemyHealth;
    private GameObject enemyPrefab;

}
