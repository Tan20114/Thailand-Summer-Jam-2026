using UnityEngine;

public class MovingWater : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    BoxCollider2D collider => GetComponent<BoxCollider2D>();

    bool reach = false;
    [SerializeField] float speed = 5f;
    [SerializeField] Vector3 size;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider.enabled = false;
        rb.linearVelocity = (transform.eulerAngles.z == 90 ? Vector2.right : Vector2.down) * speed;
    }

    private void Update()
    {
        if(transform.eulerAngles.z != 90)
        {
            if (transform.position.y <= .1f)
            {
                if (reach) return;

                reach = true;

                collider.enabled = true;
                transform.position = new Vector2(transform.position.x, 0);
                rb.linearVelocity = Vector2.zero;
                rb.linearVelocity = (Random.Range(0,2) == 0 ? Vector2.right : Vector2.left) * speed / 2.5f;
            }
        }
        else
        {
            transform.localScale = size;
            if (transform.position.x >= .1f)
            {
                if (reach) return;

                reach = true;

                collider.enabled = true;
                transform.position = new Vector2(0, transform.position.y);
                rb.linearVelocity = Vector2.zero;
                rb.linearVelocity = (Random.Range(0,2) == 0 ? Vector2.up : Vector2.down) * speed / 2.5f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity *= -1;
        }
    }
}