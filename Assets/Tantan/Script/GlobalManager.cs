using System.Collections;
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

    private void Start()
    {
        enterTime++;

        LoadProgress();
        StartCoroutine(AutoSave());
    }

    IEnumerator AutoSave()
    {
        while (true)
        {
            SaveProgress();
            yield return new WaitForSecondsRealtime(120f);
        }
    }

    void SaveProgress()
    {
        Debug.Log("Saved");

        PlayerPrefs.SetInt("enterTime", enterTime);

        PlayerPrefs.SetInt("tutorialIn", tutorialIn ? 1 : 0);
        PlayerPrefs.SetInt("tutorialCleared", tutorialCleared ? 1 : 0);
        PlayerPrefs.SetInt("day1Cleared", day1Cleared ? 1 : 0);
        PlayerPrefs.SetInt("day2Cleared", day2Cleared ? 1 : 0);
        PlayerPrefs.SetInt("day3Cleared", day3Cleared ? 1 : 0);

        PlayerPrefs.SetFloat("tutorialProgress", tutorialProgress);
        PlayerPrefs.SetFloat("day1Progress", day1Progress);
        PlayerPrefs.SetFloat("day2Progress", day2Progress);
        PlayerPrefs.SetFloat("day3Progress", day3Progress);

        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        Debug.Log("Load");

        enterTime = PlayerPrefs.GetInt("enterTime", 0);

        tutorialIn = PlayerPrefs.GetInt("tutorialIn", 0) == 1;
        tutorialCleared = PlayerPrefs.GetInt("tutorialCleared", 0) == 1;
        day1Cleared = PlayerPrefs.GetInt("day1Cleared", 0) == 1;
        day2Cleared = PlayerPrefs.GetInt("day2Cleared", 0) == 1;
        day3Cleared = PlayerPrefs.GetInt("day3Cleared", 0) == 1;

        tutorialProgress = PlayerPrefs.GetFloat("tutorialProgress", 0f);
        day1Progress = PlayerPrefs.GetFloat("day1Progress", 0f);
        day2Progress = PlayerPrefs.GetFloat("day2Progress", 0f);
        day3Progress = PlayerPrefs.GetFloat("day3Progress", 0f);
    }
}