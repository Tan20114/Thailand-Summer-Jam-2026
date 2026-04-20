using UnityEngine;

public class ExpandingWater : MonoBehaviour
{
    [SerializeField] bool isSpin = false;

    [SerializeField] float growTime = 0.1f;
    [SerializeField] float shrinkTime = 0.75f;

    [SerializeField] float lifetime = 5f;
    [SerializeField] float spinSpeed = 45f;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        float tLife = timer / lifetime;

        HandleScale(tLife);
        Spin();

        if (tLife >= 1f)
            Destroy(gameObject);
    }

    void HandleScale(float tLife)
    {
        float value = 1f;

        if (tLife <= growTime)
        {
            float t = tLife / growTime;
            float eased = Mathf.Pow(t, 3);

            value = Mathf.Lerp(0f, 2.5f, eased);
        }
        else if (tLife <= shrinkTime)
        {
            value = 2.5f;
        }
        else
        {
            float t = (tLife - shrinkTime) / (1f - shrinkTime);
            float eased = 1f - Mathf.Pow(1f - t, 3);

            value = Mathf.Lerp(2.5f, 0f, eased);
        }

        transform.localScale = new Vector3(1, value, 1);
    }

    void Spin()
    {
        if (!isSpin) return;

        transform.eulerAngles = new Vector3(
            0,
            0,
            transform.eulerAngles.z + spinSpeed * Time.deltaTime
        );
    }

    public float GetGrowTime() => growTime;
    public float GetShrinkTime() => shrinkTime;
    public float GetLifetime() => lifetime;
}