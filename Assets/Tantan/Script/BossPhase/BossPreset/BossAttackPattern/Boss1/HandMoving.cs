using System.Collections;
using UnityEngine;

public class HandMoving : MonoBehaviour
{
    public BoxCollider2D collider => GetComponent<BoxCollider2D>();
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] float moveSpeed = 5f;
    public float waitTime = 5f;
    [SerializeField] float lifeTime = 10f;
    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveCoroutine(waitTime));
        Destroy(gameObject, lifeTime);
    }

    IEnumerator MoveCoroutine(float wait)
    {
        yield return new WaitForSeconds(waitTime);
        Vector2 dir = target.position - transform.position;
        Vector2 normalizedDir = dir.normalized;

        rb.linearVelocity = normalizedDir * moveSpeed;
    }
}