using UnityEngine;
using System.Collections;
using Lean.Pool;

public class PewPew1 : AttackTemplate
{
    LeanGameObjectPool pool => GameObject.FindGameObjectWithTag("BulletPool").GetComponent<LeanGameObjectPool>();

    Coroutine running;

    Transform spawnPoint;
    MonoBehaviour owner;

    float interval = 1f;

    public PewPew1(GameObject projectile, Transform spawnPoint, MonoBehaviour owner)
    {
        this.spawnPoint = spawnPoint;
        this.owner = owner;
    }

    public void Execute()
    {
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
            pool.Spawn(spawnPoint.position, spawnPoint.rotation);
        }
    }
}