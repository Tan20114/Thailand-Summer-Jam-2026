using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;

    [SerializeField] float shakeDuration = 0f;
    [SerializeField] float shakeMagnitude = 0.1f;
    [SerializeField] float dampingSpeed = 1.0f;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}