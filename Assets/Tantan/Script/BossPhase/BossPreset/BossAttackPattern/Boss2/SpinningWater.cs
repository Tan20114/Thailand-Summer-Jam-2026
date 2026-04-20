using Lean.Pool;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpinningWater : AttackTemplate
{
    Transform[] spawnPoint;
    MonoBehaviour owner;

    GameObject projectilePrefab;

    int type = 0;

    public SpinningWater(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner ,int type)
    {
        this.spawnPoint = spawnPoint;
        projectilePrefab = projectile;
        this.owner = owner;
        this.type = type;
    }

    public void Execute()
    {
        owner.StartCoroutine(DelayForAttack(2f));
    }

    public void Stop()
    {
    }

    IEnumerator DelayForAttack(float duration)
    {
        yield return new WaitForSeconds(duration);

        switch (type)
        {
            case 1:
                for (int i = 0; i < spawnPoint.Length; i++)
                {
                    Object.Instantiate(projectilePrefab, BossBehavior.Instance.transform.position, Quaternion.Euler(0, 0, i * 90));
                }
                break;
            case 2:
                for (int i = 0; i < spawnPoint.Length; i++)
                {
                    Object.Instantiate(projectilePrefab, spawnPoint[i].position, spawnPoint[i].rotation);
                }
                break;
            case 3:
                for (int i = 0; i < spawnPoint.Length; i++)
                {
                    Object.Instantiate(projectilePrefab, spawnPoint[i].position, Quaternion.Euler(0, 0, i * 90));
                }
                break;
            case 4:
                for (int i = 1; i < 2; i++)
                {
                    Object.Instantiate(projectilePrefab, BossBehavior.Instance.transform.position, Quaternion.Euler(0, 0, i * 90));
                }
                break;
            case 5:
                for (int i = 0; i < 6 ; i++)
                {
                    owner.StartCoroutine(Radiant(i));
                }
                break;
        }
        
        IEnumerator Radiant(int index)
        {
            ExpandingWater ew = Object.Instantiate(projectilePrefab, BossBehavior.Instance.transform.position, Quaternion.Euler(0, 0, index * 60)).GetComponent<ExpandingWater>();
            yield return new WaitForSeconds(ew.GetLifetime() * (ew.GetGrowTime() - .1f));
            Object.Instantiate(projectilePrefab, BossBehavior.Instance.transform.position, Quaternion.Euler(0, 0, index * 60 + 30));
        }
    }
}
