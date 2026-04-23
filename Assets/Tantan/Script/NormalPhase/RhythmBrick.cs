using Lean.Pool;
using System.Collections;
using UnityEngine;

public class RhythmBrick : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    BoxCollider2D collider => GetComponent<BoxCollider2D>();

    public static int id = 1;

    [Header("Reference")]
    [SerializeField] LeanGameObjectPool pool;
    [SerializeField] LayerMask perfectMask;
    [SerializeField] LayerMask goodMask;
    [SerializeField] LayerMask badMask;

    [Header("Properties")]
    [SerializeField] Vector2 size = Vector2.one;
    [SerializeField] float speed = 10;

    float elaspedTime = 0;

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

    private void Update()
    {
        elaspedTime += Time.deltaTime;
    }

    public void DespawnBlock()
    {
        NormalPhaseManager.Instance.allBrick.Remove(this);
        NormalPhaseManager.Instance.OnNoteChanged();
        pool.Despawn(gameObject);
    }

    public void Check()
    {
        Debug.Log($"Note[{BrickSpawner.count++}] {(elaspedTime > 3.9f ? "Late" : "Before")} by : {Mathf.Abs(elaspedTime - 3.9f)}");
        elaspedTime = 0;

        if (Physics2D.OverlapBox(transform.position, size, 0f, perfectMask))
        {
            NormalPhaseManager.Instance.OnPerfect();
            ComboPoolSpawner.instance.Spawn(0,NormalPhaseManager.Instance.currentMinigame.comboPos, Quaternion.identity);
        }
        else if (Physics2D.OverlapBox(transform.position, size, 0f, goodMask))
        {
            NormalPhaseManager.Instance.OnGood();
            ComboPoolSpawner.instance.Spawn(1,NormalPhaseManager.Instance.currentMinigame.comboPos, Quaternion.identity);
        }
        else if (Physics2D.OverlapBox(transform.position, size, 0f, badMask))
        {
            NormalPhaseManager.Instance.OnBad();
            ComboPoolSpawner.instance.Spawn(2,NormalPhaseManager.Instance.currentMinigame.comboPos, Quaternion.identity);
        }
        else
        {
            NormalPhaseManager.Instance.OnMiss();
            ComboPoolSpawner.instance.Spawn(3,NormalPhaseManager.Instance.currentMinigame.comboPos, Quaternion.identity);
        }

        DespawnBlock();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}