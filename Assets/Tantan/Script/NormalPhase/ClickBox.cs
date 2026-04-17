using UnityEngine;

public class ClickBox : MonoBehaviour
{
    BoxCollider2D collider => GetComponent<BoxCollider2D>();

    [SerializeField] Color color = Color.white;
    [SerializeField] Vector2 size = Vector2.one;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider.size = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
