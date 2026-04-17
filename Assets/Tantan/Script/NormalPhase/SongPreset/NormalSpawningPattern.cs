using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NormalSpawningPattern", menuName = "Normal Phase/NormalSpawningPattern")]
public class NormalSpawningPattern : ScriptableObject
{
    public float halfTime;
    public List<float> spawnTime;
}
