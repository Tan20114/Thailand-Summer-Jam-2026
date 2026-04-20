using Lean.Pool;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScatterBullet : Bullet
{
    [Header("References")]
    [SerializeField] GameObject smolBulletPrefab;
    [SerializeField] GameObject areaDamagePrafab;
    [SerializeField] float areaLifetime = 2f;

    private void OnEnable()
    {
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        rb.linearVelocity = dir * speed;

        Despawn(lifeTime);
    }

    private void OnDisable()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle =  (Mathf.PI / 4) + (i * Mathf.PI * 2 / 8);

            angle += BossBehavior.Instance.transform.eulerAngles.z * Mathf.Deg2Rad;

            Instantiate(smolBulletPrefab, transform.position, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
        }

        GameObject areaDam = Instantiate(areaDamagePrafab, transform.position, Quaternion.identity);
        Destroy(areaDam, areaLifetime);
    }

    public override void Despawn(float lifetime)
    {
        Destroy(gameObject, lifetime);
    }
}