using TMPro;
using UnityEngine;

public class EndGameVisual : MonoBehaviour
{
    [Header("RatingCount")]
    [SerializeField] TextMeshProUGUI perfectCount;
    [SerializeField] TextMeshProUGUI goodCount;
    [SerializeField] TextMeshProUGUI badCount;
    [SerializeField] TextMeshProUGUI missCount;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI comboText;

    [Header("Quote")]
    [SerializeField] TextMeshProUGUI quoteText;
    [SerializeField] TextMeshProUGUI byText;
    [SerializeField] Quote quoteList;

    // Update is called once per frame
    void Update()
    {
        perfectCount.text = "Perfect : " + GameManager.Instance.perfectCount.ToString();
        goodCount.text = "Good : " + GameManager.Instance.goodCount.ToString();
        badCount.text = "Bad : " + GameManager.Instance.badCount.ToString();
        missCount.text = "Miss : " + GameManager.Instance.missCount.ToString();

        scoreText.text = GameManager.Instance.score.ToString("F2") + "%";
        healthText.text = "Health : " + GameManager.Instance.health;
        comboText.text = "Max Combo : " + GameManager.Instance.maxCombo.ToString();

        quoteText.text = quoteList.quotes[(int)(GameManager.Instance.score / 10)][0];
        byText.text = quoteList.quotes[(int)(GameManager.Instance.score / 10)][1];
    }
}
