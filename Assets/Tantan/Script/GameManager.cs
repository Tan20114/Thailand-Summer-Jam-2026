using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Action OnGameEnded;

    [Header("References")]
    [SerializeField] int index = 0;
    [SerializeField] GameObject normalPhase;
    [SerializeField] GameObject bossPhase;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject tutorialPage;
    [SerializeField] CanvasGroup pausePage;
    [SerializeField] BossLoader bl;
    [SerializeField] AudioSource source;

    [Header("Timer")]
    public float elaspedTime = 0f;
    public int step = 0;
    [SerializeField] float normalPhaseDuration = 30f;
    public float score = 0;
    [SerializeField] int noteCount;

    #region RatingCount
    [Header("Rating Count")]
    public int perfectCount = 0;
    public int goodCount = 0;
    public int badCount = 0;
    public int missCount = 0;
    #endregion

    [Header("Game State")]
    public int combo = 0;
    public int maxCombo = 0;
    public string health = "";

    bool isBossLoaded = false;
    bool isGameEnded = false;
    [HideInInspector] public bool isTutorial = false;
    bool isPause = false;

    private void Start()
    {
        normalPhaseDuration = NormalPhaseManager.Instance.song.halfTime;
        if (index == 0 && !GlobalManager.Instance.tutorialIn)
        {
            tutorialPage.SetActive(true);
        }
    }

    void Update()
    {
        elaspedTime = source.time;

        if (elaspedTime < normalPhaseDuration)
        {
            normalPhase.SetActive(true);
            bossPhase.SetActive(false);
        }
        else
        {
            NormalPhaseManager.Instance.isNormalPhase = false;
            NormalPhaseManager.Instance.ResetMinigame();
            normalPhase.SetActive(false);
            bossPhase.SetActive(true);
            LoadBoss();
        }

        ComboCounter();

        if (!source.isPlaying && !isGameEnded && !isTutorial && !isPause)
        {
            EndGame(true);
        }
    }

    void LoadBoss()
    {
        if (isBossLoaded) return;
        isBossLoaded = true;
        
        bl.Load();
    }

    void ComboCounter()
    {
        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
    }

    public void EndGame(bool isClear)
    {
        Debug.Log("Game Ended");

        isGameEnded = true;
        source.Stop();

        bossPhase.SetActive(false);
        normalPhase.SetActive(false);
        resultPanel.SetActive(true);

        UpdateGlobal(isClear);

        OnGameEnded?.Invoke();
    }

    void UpdateGlobal(bool isCleared)
    {
        switch (index)
        {
            case 0:
                GlobalManager.Instance.tutorialCleared = isCleared;
                GlobalManager.Instance.tutorialProgress = score;
                break;
            case 1:
                GlobalManager.Instance.day1Cleared = isCleared;
                GlobalManager.Instance.day1Progress = score;
                break;
            case 2:
                GlobalManager.Instance.day2Cleared = isCleared;
                GlobalManager.Instance.day2Progress = score;
                break;
            case 3:
                GlobalManager.Instance.day3Cleared = isCleared;
                GlobalManager.Instance.day3Progress = score;
                break;
        }
    }

    public void CalculateScore(int type)
    {
        float perfectScore = 50/noteCount;

        score += type switch
        {
            0 => perfectScore, // Perfect
            1 => perfectScore * 0.7f,  // Good
            2 => perfectScore * 0.5f,  // Bad
            3 => 0,   // Miss
            _ => 0
        };
    }

    public void TogglePause()
    {
        isPause = !isPause;

        if (isPause)
            Pause();
        else
            Unpause();
    }

    void Pause()
    {
        isPause = true;

        pausePage.alpha = 1;

        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    void Unpause()
    {
        isPause = false;

        pausePage.alpha = 0;

        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (isTutorial) return;

        if (!focus)
            Pause();
        else
            Unpause();
    }
}
