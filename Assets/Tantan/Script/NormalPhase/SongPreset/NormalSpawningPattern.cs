using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class SpawnData
{
    public bool type;
    public float time;
}

[CreateAssetMenu(fileName = "NormalSpawningPattern", menuName = "Normal Phase/NormalSpawningPattern")]
public class NormalSpawningPattern : ScriptableObject
{
    public float halfTime;
    public List<float> spawnTime;
}
