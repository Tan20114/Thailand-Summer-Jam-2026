using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField] GameObject normalPhase;
    [SerializeField] GameObject bossPhase;
    [SerializeField] BossLoader bl;
    [SerializeField] AudioSource source;

    [Header("Timer")]
    public float elaspedTime = 0f;
    public int step = 0;
    [SerializeField] float normalPhaseDuration = 30f;

    bool isBossLoaded = false;

    private void Start()
    {
        normalPhaseDuration = NormalPhaseManager.Instance.song.halfTime;
    }

    void Update()
    {
        //elaspedTime += Time.deltaTime;
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
    }

    void LoadBoss()
    {
        if (isBossLoaded) return;
        isBossLoaded = true;
        bl.Load();
    }
}
