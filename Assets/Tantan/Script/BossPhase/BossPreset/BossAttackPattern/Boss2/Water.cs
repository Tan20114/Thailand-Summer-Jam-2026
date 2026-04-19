using Unity.VisualScripting;
using UnityEngine;

public class Water : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] float speed = 5f;
    [SerializeField] float lifetime = 5f;
    [SerializeField] Vector3 size;

    bool reach = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = (transform.eulerAngles.z == 90 ? Vector2.right : Vector2.down) * speed;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if(transform.eulerAngles.z != 90)
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