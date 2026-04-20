using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
    LeanGameObjectPool pool => GameObject.FindGameObjectWithTag("WaterPol").GetComponent<LeanGameObjectPool>();

    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] bool isPooled;
    [SerializeField] float speed = 5f;
    [SerializeField] float lifetime = 5f;
    [SerializeField] Vector3 size;

    bool reach = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isPooled)
            pool.Despawn(gameObject, lifetime);
        else
            Destroy(gameObject, lifetime);
    }

    private void OnEnable()
    {
        rb.linearVelocity = (transform.eulerAngles.z == 90 ? Vector2.right : Vector2.down) * speed;
        pool.Despawn(gameObject, lifetime);
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (isPooled) return;

        if (transform.eulerAngles.z != 90)
        {
            if (transform.position.y <= .1f)
            {
                if (reach) return;

                reach = true;
                transform.position = new Vector2(transform.position.x, 0);
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            transform.localScale = size;
            if (transform.position.x >= .1f)
            {
                if (reach) return;

                reach = true;
                transform.position = new Vector2(0, transform.position.y);
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}