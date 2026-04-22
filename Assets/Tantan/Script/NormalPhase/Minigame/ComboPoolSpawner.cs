using Lean.Pool;
using UnityEngine;

public class ComboPoolSpawner : MonoBehaviour
{
    public static ComboPoolSpawner instance;

    [Header("Properties")]
    [SerializeField] float lifeTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] comboSprite;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    LeanGameObjectPool pool => GetComponent<LeanGameObjectPool>();

    public void Spawn(int type,Vector3 position, Quaternion rotation)
    {
        GameObject go = pool.Spawn(position, rotation);
        go.GetComponent<SpriteRenderer>().sprite = comboSprite[type];
        pool.Despawn(go, lifeTime);
    }
}
