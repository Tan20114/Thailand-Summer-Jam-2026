using UnityEngine;

enum type
{
    Pattern1,
    Pattern2,
    Pattern3
}

public class WaterPeedPeed : AttackTemplate
{
    GameObject projectilePrefab;
    Transform[] spawnPoints;
    MonoBehaviour owner;

    public WaterPeedPeed(GameObject projectile, Transform[] spawnPoint, MonoBehaviour owner)
    {
        projectilePrefab = projectile;
        spawnPoints = spawnPoint;
        this.owner = owner;
    }

    void Pattern1()
    {
        for (int i = 0; i <= 16; i += 2)
        {
            Object.Instantiate(projectilePrefab, new Vector2(spawnPoints[i].transform.position.x, 12), Quaternion.identity);
        }
    }

    void Pattern2()
    {
        for (int i = 1; i <= 16; i += 2)
        {
            Object.Instantiate(projectilePrefab, new Vector2(spawnPoints[i].transform.position.x, 12), Quaternion.identity);
        }
    }

    void Pattern3()
    {
        for (int i = 17; i <= spawnPoints.Length; i += 2)
        {
            Object.Instantiate(projectilePrefab, new Vector2(-20, spawnPoints[i].transform.position.y), Quaternion.Euler(0, 0, 90));
        }
    }

    void Pattern4()
    {
        for (int i = 18; i <= spawnPoints.Length - 1; i += 2)
        {
            Object.Instantiate(projectilePrefab, new Vector2(-20, spawnPoints[i].transform.position.y), Quaternion.Euler(0, 0, 90));
        }
    }

    public void Execute()
    {
        Pattern1();
    }

    public void Stop()
    {

    }
}