using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public static Knockback Instance;
    private Rigidbody enemyRb;
    private Coroutine routine;

    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.25f;

    public bool isKnockedback {get; private set;}

    private void Awake()
    {
        Instance = this;
        enemyRb = GetComponent<Rigidbody>();
    }


    public void ApplyKnockback(Vector3 startDir)
    {
        Vector3 dir = startDir + Vector3.forward;
        dir.Normalize();

        if(routine != null) StopCoroutine(routine);
        routine = StartCoroutine(KnockbackEnumerator(dir));
    }

    private IEnumerator KnockbackEnumerator(Vector3 dir)
    {
        isKnockedback = true;

        enemyRb.linearVelocity = Vector3.zero;
        enemyRb.AddForce(dir * knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        enemyRb.linearVelocity = new Vector3(0f, enemyRb.linearVelocity.y, 0f);
        isKnockedback = false;

    }


}
