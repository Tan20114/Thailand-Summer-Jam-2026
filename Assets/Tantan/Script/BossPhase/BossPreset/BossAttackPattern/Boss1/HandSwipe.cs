using UnityEngine;
using System.Collections;

public class HandSwipe : AttackTemplate
{
    Coroutine running;

    GameObject projectilePrefab;
    Transform[] spawnPoints;
    int first, second;
    int alt1,alt2;
    Transform spawnPoint1;
    Transform spawnPoint2;
    MonoBehaviour owner;

    float interval = 3f;

    public HandSwipe(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
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
            RandomSpawnPoint();
            Spawn();

            yield return new WaitForSeconds(interval);
        }
    }

    void Spawn()
    {
        GameObject hand;
        if (first > 3)
            hand = Object.Instantiate(projectilePrefab, spawnPoint1.position, Quaternion.Euler(0,0,90));
        else
            hand = Object.Instantiate(projectilePrefab, spawnPoint1.position, Quaternion.identity);

        hand.GetComponent<HandMoving>().target = spawnPoints[alt1];

        if (second > 3)
            hand = Object.Instantiate(projectilePrefab, spawnPoint2.position, Quaternion.Euler(0, 0, 90));
        else
            hand = Object.Instantiate(projectilePrefab, spawnPoint2.position, Quaternion.identity);

        hand.GetComponent<HandMoving>().target = spawnPoints[alt2];
    }

    void RandomSpawnPoint()
    {
        first = Random.Range(0, spawnPoints.Length);

        if (first % 2 == 0) alt1 = first + 1;
        else alt1 = first - 1;

        do
        {
            second = Random.Range(0, spawnPoints.Length);
        }
        while (second == first);

        if (second % 2 == 0) alt2 = second + 1;
        else alt2 = second - 1;

        spawnPoint1 = spawnPoints[first];
        spawnPoint2 = spawnPoints[second];
    }
}
