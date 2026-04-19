using System.Collections;
using UnityEngine;

public class ShockwaveAttack : AttackTemplate
{
    GameObject projectilePrefab;
    MonoBehaviour owner;

    float interval = 3f;

    public ShockwaveAttack(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
    {
        projectilePrefab = projectile;
        this.owner = owner;
    }

    public void Execute()
    {
        owner.StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(interval);

        for (int i = 0; i < 3; i++)
        {
            Object.Instantiate(
                projectilePrefab,
                BossBehavior.Instance.transform.position,
                Quaternion.identity
            );

            yield return new WaitForSeconds(interval);
        }
    }

    public void Stop()
    {

    }
}