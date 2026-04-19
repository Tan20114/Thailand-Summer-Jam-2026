using Lean.Pool;
using System.Collections;
using UnityEngine;

public class RhythmBrick : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    BoxCollider2D collider => GetComponent<BoxCollider2D>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();

    public static int id = 1;

    [Header("Reference")]
    [SerializeField] LeanGameObjectPool pool;
    [SerializeField] LayerMask perfectMask;
    [SerializeField] LayerMask goodMask;
    [SerializeField] LayerMask badMask;

    [Header("Properties")]
    [SerializeField] Vector2 size = Vector2.one;
    [SerializeField] float speed = 10;

    private void Start()
    {
        gameObject.name = $"note {id++}";
    }

    private void OnEnable()
    {
        collider.size = size;
        rb.linearVelocity = Vector2.right * speed;
        pool = GameObject.FindGameObjectWithTag("BrickPool").GetComponent<LeanGameObjectPool>();
    }

    public void DespawnBlock()
    {
        NormalPhaseManager.Instance.allBrick.Remove(this);
        pool.Despawn(gameObject);
    }

    public void Check()
    {
        if (Physics2D.OverlapBox(transform.position, size, 0f, perfectMask))
            Debug.Log("perfect");
        else if (Physics2D.OverlapBox(transform.position, size, 0f, goodMask))
            Debug.Log("good");
        else if (Physics2D.OverlapBox(transform.position, size, 0f, badMask))
            Debug.Log("bad");
        else
            Debug.Log("Miss");

        DespawnBlock();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}