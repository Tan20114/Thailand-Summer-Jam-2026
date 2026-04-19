using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject normalPhase;
    [SerializeField] GameObject bossPhase;

    [Header("Timer")]
    float elaspedTime = 0;
    [SerializeField] float normalPhaseDuration = 30f;

    // Update is called once per frame
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
            normalPhase.SetActive(false);
            bossPhase.SetActive(true);
        }
    }
}
