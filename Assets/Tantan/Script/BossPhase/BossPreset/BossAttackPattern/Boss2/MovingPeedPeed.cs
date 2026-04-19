using UnityEngine;

public class MovingPeedPeed : AttackTemplate
{
    GameObject projectilePrefab;
    Transform[] spawnPoints;

    GameObject spawnedWater;

    public MovingPeedPeed(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
    {
        projectilePrefab = projectile;
        spawnPoints = spawnPoint;
    }

    public void Execute()
    {
        int ranVal = Random.Range(0, spawnPoints.Length);

        if (spawnPoints[ranVal].gameObject.name.Contains("t"))
            spawnedWater = Object.Instantiate(projectilePrefab, new Vector2(spawnPoints[ranVal].position.x, 12), Quaternion.identity);
        else
            spawnedWater = Object.Instantiate(projectilePrefab, new Vector2(-20, spawnPoints[ranVal].position.y), Quaternion.Euler(0, 0, 90));
    }

    public void Stop()
    {
        Object.Destroy(spawnedWater);
    }
}
