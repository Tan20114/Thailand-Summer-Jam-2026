using UnityEngine;
using System.Collections;
using Lean.Pool;

public class PewPew2 : AttackTemplate
{
    LeanGameObjectPool pool => GameObject.FindGameObjectWithTag("BulletPool").GetComponent<LeanGameObjectPool>();

    Coroutine running;

    Transform spawnPoint;
    MonoBehaviour owner;

    float interval = .25f;
    float zRotationOffset = 15f;

    public PewPew2(GameObject projectile, Transform spawnPoint, MonoBehaviour owner)
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
            for (int i = 0; i < 3; i++)
            {
                pool.Spawn(spawnPoint.position, Quaternion.Euler(0f, 0f, spawnPoint.eulerAngles.z + ((i % 2 == 0) ? zRotationOffset : -zRotationOffset)));
            }           
        }
    }
}