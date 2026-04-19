using Unity.VisualScripting;
using UnityEngine;

public class ShockwaveExpand : MonoBehaviour
{
    EdgeCollider2D edgeCollider => GetComponent<EdgeCollider2D>();

    [SerializeField] float expandSpeed = 5f;
    [SerializeField] float lifetime = 5f;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
        edgeCollider.edgeRadius = transform.localScale.x / 30;
    }
}