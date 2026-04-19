using System.Collections;
using UnityEngine;

public class EyeShoot : AttackTemplate
{
    GameObject projectilePrefab;
    Transform spawnPoint;
    MonoBehaviour owner;

    public EyeShoot (GameObject projectile, Transform spawnPoint, MonoBehaviour owner)
    {
        projectilePrefab = projectile;
        this.spawnPoint = spawnPoint;
        this.owner = owner;

    }

    public void Execute() => owner.StartCoroutine(Spawn());

    public void Stop()
    {
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        Object.Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
    }
}
