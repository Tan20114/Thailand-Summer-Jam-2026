using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class HandStompAttack : AttackTemplate
{
    Coroutine running;

    GameObject projectilePrefab;
    Transform[] spawnPoints;
    int first, second;
    int alt1, alt2;
    Transform spawnPoint1;
    Transform spawnPoint2;
    MonoBehaviour owner;

    float interval = 2f;

    public HandStompAttack(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
    {
        projectilePrefab = projectile;
        spawnPoints = spawnPoint;
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
            Object.Instantiate(projectilePrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

            yield return new WaitForSeconds(interval);
        }
    }
}
