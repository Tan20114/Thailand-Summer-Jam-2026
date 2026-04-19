using UnityEngine;

public class ClickBox : MonoBehaviour
{
    BoxCollider2D collider => GetComponent<BoxCollider2D>();

    [SerializeField] Color color = Color.white;
    [SerializeField] Vector2 size = Vector2.one;

    void Start()
    {
        collider.size = size;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
