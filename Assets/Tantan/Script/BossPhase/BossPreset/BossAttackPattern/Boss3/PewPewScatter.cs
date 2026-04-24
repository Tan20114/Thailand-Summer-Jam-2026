using System.Collections;
using UnityEngine;

public class PewPewScatter : AttackTemplate
{
    Coroutine running;

    Transform spawnPoint;
    GameObject toShoot;
    MonoBehaviour owner;

    float interval = 1.25f;

    public PewPewScatter(GameObject projectile, Transform spawnPoint, MonoBehaviour owner)
    {
        toShoot = projectile;
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
            Object.Instantiate(toShoot,spawnPoint.position, spawnPoint.rotation);
        }
    }
}
