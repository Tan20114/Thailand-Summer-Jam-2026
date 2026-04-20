using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public enum ShakeStyle
    {
        Smooth,
        Vigorous
    }

    [Header("Settings")]
    [SerializeField] ShakeStyle style = ShakeStyle.Smooth;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float magnitude = 0.2f;
    [SerializeField] float frequency = 20f;

    Vector3 originalPos;
    float timer = 0f;
    bool isShaking = false;
    float noiseTime;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (!isShaking) return;

        timer += Time.deltaTime;

        float t = timer / duration;

        if (timer >= duration)
        {
            StopShake();
            return;
        }

        float strength = Evaluate(t) * magnitude;

        Vector3 offset = GetShakeOffset(strength);
        transform.localPosition = originalPos + offset;
    }

    float Evaluate(float t)
    {
        switch (style)
        {
            case ShakeStyle.Smooth:
                return Mathf.SmoothStep(1f, 0f, t);

            case ShakeStyle.Vigorous:
                return Mathf.Abs(Mathf.Sin(t * Mathf.PI * frequency)) * (1f - t);

            default:
                return 1f - t;
        }
    }

    Vector3 GetShakeOffset(float strength)
    {
        if (style == ShakeStyle.Smooth)
        {
            noiseTime += Time.deltaTime * frequency;

            float x = Mathf.PerlinNoise(noiseTime, 0f) * 2f - 1f;
            float y = Mathf.PerlinNoise(0f, noiseTime) * 2f - 1f;

            return new Vector3(x, y, 0f) * strength;
        }
        else
        {
            return new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f
            ) * strength;
        }
    }

    public void Shake(float duration, float magnitude, ShakeStyle style)
    {
        this.duration = duration;
        this.magnitude = magnitude;
        this.style = style;

        timer = 0f;
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
        transform.localPosition = originalPos;
    }
}