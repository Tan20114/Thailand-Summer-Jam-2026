using Lean.Pool;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    #region References
    int id = 1;
    int ID
    {
        get => id;
        set
        {
            if (id >= pool.Capacity)
                id = 1;
            else
                id = value;
        }
    }

    LeanGameObjectPool pool => GetComponent<LeanGameObjectPool>();
    #endregion

    float targetTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => targetTime = NormalPhaseManager.Instance.song.spawnTime[GameManager.Instance.step];

    void Update()
    {
        if (!NormalPhaseManager.Instance.isNormalPhase) return;
        if (GameManager.Instance.step > NormalPhaseManager.Instance.song.spawnTime.Count - 1) return;

        if (GameManager.Instance.elaspedTime > targetTime)
        {
            SpawnBrick();

            GameManager.Instance.step++;

            if (GameManager.Instance.step < NormalPhaseManager.Instance.song.spawnTime.Count)
                targetTime = NormalPhaseManager.Instance.song.spawnTime[GameManager.Instance.step];
        }
    }

    void SpawnBrick()
    {
        GameObject obj = pool.Spawn(transform.position, Quaternion.identity);
        NormalPhaseManager.Instance.allBrick.Add(obj.GetComponent<RhythmBrick>());
    }
}