using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quote", menuName = "Normal Phase/Quote")]
public class Quote : ScriptableObject
{
    [SerializedDictionary("Percent","Quotes")]
    public SerializedDictionary<int,List<string>> quotes;
}
