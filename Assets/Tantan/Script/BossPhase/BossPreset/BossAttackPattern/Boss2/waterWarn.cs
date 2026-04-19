using UnityEngine;

public class waterWarn : MonoBehaviour
{
    [SerializeField] GameObject warning;

    private void Start()
    {
        warning = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(transform.position, Vector2.one, 0, LayerMask.GetMask("Danger")))
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector2.one);
    }
}