using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField] GameObject normalPhase;
    [SerializeField] GameObject bossPhase;

    [Header("Timer")]
    public float elaspedTime = 0f;
    public int step = 0;
    [SerializeField] float normalPhaseDuration = 30f;

    void Update()
    {
        elaspedTime += Time.deltaTime;

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
        }
    }
}
