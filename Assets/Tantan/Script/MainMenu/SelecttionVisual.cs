using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AYellowpaper.SerializedCollections;
using TMPro;

public class SelectionVisual : MonoBehaviour
{
    int index = 0;

    [Header("Element")]
    [SerializeField] Image cd;
    [SerializeField] float rotationSpeed = 30;
    [SerializeField] Sprite[] cdSprite;
    [SerializeField] TextMeshProUGUI songName;
    [SerializeField] TextMeshProUGUI progress;
    [SerializeField] Image bg;
    [SerializedDictionary("Index","BG")]
    [SerializeField]  SerializedDictionary<int, Sprite[]> bgSprite;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip;


    private void Start()
    {
        index = 0;
        cd.sprite = cdSprite[index];
        audioSource.clip = audioClip[index];
        bg.sprite = bgSprite[index][Random.Range(0, bgSprite[index].Length)];
        songName.text = "New Game+";
        progress.text = GlobalManager.Instance.tutorialProgress.ToString("F2") + "%";
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        cd.rectTransform.rotation = Quaternion.Euler(0, 0, cd.rectTransform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
    }
}
