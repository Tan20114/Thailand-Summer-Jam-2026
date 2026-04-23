using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action OnGameEnded;

    [Header("References")]
    [SerializeField] GameObject normalPhase;
    [SerializeField] GameObject bossPhase;
    [SerializeField] GameObject resultPanel;
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

    private void Start()
    {
        normalPhaseDuration = NormalPhaseManager.Instance.song.halfTime;
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
            LoadBoss();
            bossPhase.SetActive(true);
        }

        ComboCounter();

        if (!source.isPlaying && !isGameEnded)
        {
            EndGame();
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

    public void EndGame()
    {
        Debug.Log("Game Ended");

        isGameEnded = true;
        source.Stop();

        bossPhase.SetActive(false);
        normalPhase.SetActive(false);
        resultPanel.SetActive(true);

        OnGameEnded?.Invoke();
        Time.timeScale = 0f;
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
}
