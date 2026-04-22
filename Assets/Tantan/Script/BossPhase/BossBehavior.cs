using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BossBehavior : Singleton<BossBehavior>
{
    [Header("References")]
    public GameObject[] projectilePrefab;
    public Transform[] innerSpawnPoints;
    public Transform[] outerSpawnPoints;

    [SerializeField] AttackQueue attackQueue;
    [SerializeField] NormalSpawningPattern song;

    [Header("Properties")]
    public bool isAttacking = false;
    [SerializeField] int phase = 1;
    AttackTemplate currentAttack;
    float attackStartTime;
    float attackStopTime;
    bool hasExecuted = false;
    float targetTime = 0;
    float step = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAttack = attackQueue.attackQueue.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        AttackByTime();
    }

    void LookAt(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void AttackByTime()
    {
        if (NormalPhaseManager.Instance.isNormalPhase) return;

        if (GameManager.Instance.step >= song.spawnTime.Count - 1)
            return;

        float currentTime = GameManager.Instance.elaspedTime;

        float start = song.spawnTime[GameManager.Instance.step];
        float end = song.spawnTime[GameManager.Instance.step + 1];

        float interval = end - start;

        attackStartTime = start;
        attackStopTime = start + interval * 0.8f;

        if (!hasExecuted && currentTime >= attackStartTime)
        {
            if (currentAttack == null && attackQueue.attackQueue.Count > 0)
                currentAttack = attackQueue.attackQueue.Dequeue();

            currentAttack?.Execute();
            hasExecuted = true;
        }

        if (hasExecuted && currentTime >= attackStopTime)
        {
            currentAttack?.Stop();
            hasExecuted = false;

            if (attackQueue.attackQueue.Count > 0)
                currentAttack = attackQueue.attackQueue.Dequeue();

            GameManager.Instance.step++;
        }
    }
}