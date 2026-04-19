using Lean.Pool;
using System.Collections;
using UnityEngine;

public class PewPewAround : AttackTemplate
{
    LeanGameObjectPool pool => GameObject.FindGameObjectWithTag("BulletPool").GetComponent<LeanGameObjectPool>();

    Coroutine running;

    Transform[] spawnPoint;
    MonoBehaviour owner;

    float interval = .25f;
    float radius = 1.5f;
    int attackCount = 0;

    public PewPewAround(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
    {
        this.spawnPoint = spawnPoint;
        this.owner = owner;
    }

    public void Execute()
    {
        attackCount = 0;
        radius = spawnPoint[0].position.x;

        running = owner.StartCoroutine(AttackCoroutine());
    }

    public void Stop()
    {
        if (running != null)
            owner.StopCoroutine(running);
    }

    IEnumerator AttackCoroutine()
    {
        while (BossBehavior.Instance.isAttacking)
        {
            yield return new WaitForSeconds(interval);

            attackCount++;

            for (int i = 0; i < spawnPoint.Length; i++)
            {
                float angle = (attackCount * Mathf.PI / 18) + (i * Mathf.PI * 2 / spawnPoint.Length);

                angle += BossBehavior.Instance.transform.eulerAngles.z * Mathf.Deg2Rad;

                pool.Spawn(BossBehavior.Instance.transform.position, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
            }
        }
    }
}