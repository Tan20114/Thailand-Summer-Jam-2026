using UnityEngine;

public class Water : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] float speed = 5f;
    [SerializeField] float lifetime = 5f;

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
            if (transform.position.y <= 0)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            transform.localScale = new Vector3(.3f, 19, 1);
            if (transform.position.x >= 0)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}