using UnityEngine;

public class GlobalManager : SingletonPersistant<GlobalManager>
{
    int enterTime = 0;

    [Header("Level Clearance")]
    public bool tutorialIn = false;
    public bool tutorialCleared = false;
    public bool day1Cleared = false;
    public bool day2Cleared = false;
    public bool day3Cleared = false;

    [Header("Level Progression")]
    public float tutorialProgress = 0f;
    public float day1Progress = 0f;
    public float day2Progress = 0f;
    public float day3Progress = 0f;
}