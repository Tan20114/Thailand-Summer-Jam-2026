using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class HeatStrokeAnim : MonoBehaviour
{
    Volume volume => GetComponent<Volume>();
    SpriteRenderer sr => GetComponentInParent<SpriteRenderer>();

    float time = 0;

    [SerializeField] float flashDuration = 0.1f;
    [SerializeField] float fadeDuration = 0.4f;

    bool isPlaying = false;

    private void OnEnable()
    {
        isPlaying = true;
        time = 0;
        volume.weight = 1;
    }

    private void Update()
    {
        if (!isPlaying) return;

        time += Time.deltaTime;

        if (time <= flashDuration)
        {
            volume.weight = 1;
        }
        else
        {
            float fadeTime = (time - flashDuration) / fadeDuration;
            volume.weight = Mathf.Lerp(1, 0, fadeTime);

            if (fadeTime >= 1f)
            {
                isPlaying = false;
            }
        }
    }

    private void OnDisable()
    {
        volume.weight = 1;
        isPlaying = false;
        time = 0;
    }
}