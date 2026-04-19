using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    LeanGameObjectPool pool => GameObject.FindGameObjectWithTag("BulletPool").GetComponent<LeanGameObjectPool>();

    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [Header("Properties")]
    [SerializeField] bool isBouncy = false;
    [SerializeField] float speed = 5;
    [SerializeField] float lifeTime = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        rb.linearVelocity = dir * speed;
        
        pool.Despawn(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!isBouncy) return;

            rb.linearVelocity *= -1;
        }
    }
}