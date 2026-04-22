using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    [SerializeField] LayerMask brickMask;
    [SerializeField] Vector2 size;

    // Update is called once per frame
    void Update()
    {
        Collider2D obj = Physics2D.OverlapBox(transform.position, size, 0f, brickMask);

        if (obj)
        {
            obj.GetComponent<RhythmBrick>().DespawnBlock();
            ComboPoolSpawner.instance.Spawn(3,NormalPhaseManager.Instance.currentMinigame.comboPos, Quaternion.identity);
            NormalPhaseManager.Instance.OnMiss();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
