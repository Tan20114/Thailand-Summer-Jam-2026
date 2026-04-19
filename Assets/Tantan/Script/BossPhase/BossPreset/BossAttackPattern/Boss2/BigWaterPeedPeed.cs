using System.Collections;
using UnityEngine;

public class BigWaterPeedPeed : AttackTemplate
{
    Coroutine running;

    GameObject projectilePrefab;
    Transform[] spawnPoints;
    MonoBehaviour owner;

    float interval = 5f;
    int prevAttack = 0;

    public BigWaterPeedPeed(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
    {
        projectilePrefab = projectile;
        spawnPoints = spawnPoint;
        this.owner = owner;
    }

    void Pattern(int index)
    {
        switch (index)
        {
            case 1:
                for (int i = 0; i <= 16; i += 2)
                {
                    Object.Instantiate(projectilePrefab, new Vector2(spawnPoints[i].transform.position.x, 12), Quaternion.identity);
                }
                break;
            case 2:
                for (int i = 1; i <= 16; i += 2)
                {
                    Object.Instantiate(projectilePrefab, new Vector2(spawnPoints[i].transform.position.x, 12), Quaternion.identity);
                }
                break;
            case 3:
                for (int i = 17; i <= spawnPoints.Length; i += 2)
                {
                    Object.Instantiate(projectilePrefab, new Vector2(-20, spawnPoints[i].transform.position.y), Quaternion.Euler(0, 0, 90));
                }
                break;
            case 4:
                for (int i = 18; i <= spawnPoints.Length - 1; i += 2)
                {
                    Object.Instantiate(projectilePrefab, new Vector2(-20, spawnPoints[i].transform.position.y), Quaternion.Euler(0, 0, 90));
                }
                break;
        }
    }

    public void Execute()
    {
        running = owner.StartCoroutine(AttackCoroutine());
    }

    public void Stop()
    {
        if (running != null)
        {
            owner.StopCoroutine(running);
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (BossBehavior.Instance.isAttacking)
        {
            int ranVal = 0;

            do
            {
                ranVal = Random.Range(1, 5);
            } 
            while (ranVal == prevAttack);

            Pattern(ranVal);
            prevAttack = ranVal;

            yield return new WaitForSeconds(interval);
        }
    }
}