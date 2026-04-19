using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : Singleton<BossBehavior>
{
    [Header("References")]
    public GameObject[] projectilePrefab;
    public Transform[] innerSpawnPoints;
    public Transform[] outerSpawnPoints;

    [SerializeField] AttackQueue attackQueue;

    [Header("Properties")]
    public bool isAttacking = false;
    [SerializeField] int phase = 1;
    [SerializeField] float attackInterval = 10f;
    AttackTemplate currentAttack;
    [SerializeField] float elapsedTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAttack = attackQueue.attackQueue.Dequeue();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }

    void LookAt(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            elapsedTime = 0f;

            isAttacking = true;
            currentAttack.Execute();

            while (elapsedTime < attackInterval * .8f)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isAttacking = false;

            yield return new WaitForSeconds(attackInterval * .2f);
            currentAttack.Stop();

            if (attackQueue.attackQueue.Count > 0 )
                currentAttack = attackQueue.attackQueue.Dequeue();
        }
    }
}