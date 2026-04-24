using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AYellowpaper.SerializedCollections;

public class MainMenuVisual : MonoBehaviour
{
    int index = 0;

    [Header("Element")]
    [SerializeField] Image cd;
    [SerializeField] float rotationSpeed = 30;
    [SerializeField] Sprite[] cdSprite;
    [SerializeField] Image bgA;
    [SerializeField] Image bgB;
    [SerializedDictionary("Index","BG")]
    [SerializeField]  SerializedDictionary<int, Sprite[]> bgSprite;
    [SerializeField] float bgChangeInterval = 5f;
    [SerializeField] float fadeDuration = 1f;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip;


    private void Start()
    {
        index = Random.Range(0, cdSprite.Length);
        cd.sprite = cdSprite[index];
        audioSource.clip = audioClip[index];
        bgA.sprite = bgSprite[index][Random.Range(0, bgSprite[index].Length)];
        StartCoroutine(BGChange(bgChangeInterval, fadeDuration));
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        cd.rectTransform.rotation = Quaternion.Euler(0, 0, cd.rectTransform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
    }

    IEnumerator BGChange(float interval, float fadeDuration)
    {
        while (true)
        {
            bgB.sprite = bgSprite[index][Random.Range(0, bgSprite[index].Length)];

            float t = 0f;

            Color aColor = bgA.color;
            Color bColor = bgB.color;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                float lerp = t / fadeDuration;

                aColor.a = 1f - lerp;
                bColor.a = lerp;

                bgA.color = aColor;
                bgB.color = bColor;

                yield return null;
            }

            var temp = bgA;
            bgA = bgB;
            bgB = temp;

            yield return new WaitForSeconds(interval);
        }
    }
}
