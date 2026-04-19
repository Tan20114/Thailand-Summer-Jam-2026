using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HandStomp : MonoBehaviour
{
    BoxCollider2D collider => GetComponent<BoxCollider2D>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();

    [Header("Properties")]
    [SerializeField] float waitTime = 5f;
    float elapsedTime = 0f;
    [SerializeField] float lifeTime = 10f;

    [Header("Warning")]
    [SerializeField] GameObject warningEffect;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr.color = new Color(1, 0, 0, 0);
        collider.enabled = true;

        warningEffect.SetActive(true);

        StartCoroutine(StompCoroutine(waitTime));
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (elapsedTime < waitTime)
            elapsedTime += Time.deltaTime;
        else
            warningEffect.SetActive(false);

        float t = elapsedTime / waitTime;
        warningEffect.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, t);
    }

    IEnumerator StompCoroutine(float wait)
    {
        yield return new WaitForSeconds(waitTime);
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1, 0, 0, 1);
        collider.enabled = true;
    }
}