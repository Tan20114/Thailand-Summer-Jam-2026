using Lean.Pool;
using UnityEngine;

public class waterWarn : MonoBehaviour
{
    LeanGameObjectPool pool => GameObject.FindGameObjectWithTag("WaterPol").GetComponent<LeanGameObjectPool>();
    LeanGameObjectPool bulletPool => GameObject.FindGameObjectWithTag("BulletPool").GetComponent<LeanGameObjectPool>();

    [SerializeField] GameObject warning;
    [SerializeField] GameObject waterPrefab;

    private void Start()
    {
        warning = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position, Vector2.one, 0, LayerMask.GetMask("Danger"));

        if (col)
        {
            if (col.gameObject.name.Contains("Shockwave")) return;

            if (col.gameObject.CompareTag("Bullet"))
            {
                SpawnWater();
                bulletPool.Despawn(col.gameObject);
            }
            else
                warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }

    private void SpawnWater()
    {
        if (gameObject.name.Contains("t"))
            pool.Spawn(new Vector2(transform.position.x, 12), Quaternion.identity);
        else
            pool.Spawn(new Vector2(-20, transform.position.y), Quaternion.Euler(0, 0, 90));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector2.one);
    }
}