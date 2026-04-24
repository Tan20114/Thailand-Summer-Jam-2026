using AYellowpaper.SerializedCollections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionButton : Button
{
    [SerializeField] int index = 0;
    bool isClear = false;
    float score = 0;

    [Header("Element")]
    [SerializeField] Image cd;
    [SerializeField] Sprite cdSprite;
    [SerializeField] Image bg;
    [SerializeField] Sprite[] bgSprite;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] TextMeshProUGUI songName;
    [SerializeField] string song;
    [SerializeField] TextMeshProUGUI progress;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClear) return;

        base.OnPointerEnter(eventData);

        cd.sprite = cdSprite;
        bg.sprite = bgSprite[Random.Range(0, bgSprite.Length)];

        songName.text = song;
        progress.text = score.ToString("F2") + "%";

        source.clip = clip;
        source.Play();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!isClear) return;

        base.OnPointerExit(eventData);
    }

    private void Update()
    {
        CheckGlobal();

        GetComponent<UnityEngine.UI.Button>().interactable = isClear;
        transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = isClear;
    }

    void CheckGlobal()
    {
        switch (index)
        {
            case 0:
                isClear = true;
                score = GlobalManager.Instance.tutorialProgress;
                break;
            case 1:
                isClear = GlobalManager.Instance.tutorialCleared;
                score = GlobalManager.Instance.day1Progress;
                break;
            case 2:
                isClear = GlobalManager.Instance.day1Cleared;
                score = GlobalManager.Instance.day2Progress;
                break;
            case 3:
                isClear = GlobalManager.Instance.day2Cleared;
                score = GlobalManager.Instance.day3Progress;
                break;
        }
    }
}