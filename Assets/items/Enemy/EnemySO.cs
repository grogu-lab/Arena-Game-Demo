using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/Enemy")]
public class EnemySO : ScriptableObject
{
    public int enemyHealth = 50;
    public GameObject enemyPrefab;

}
