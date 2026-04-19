using System.Collections;
using UnityEngine;

public class HandSwipeWarning : MonoBehaviour
{
    [SerializeField] GameObject warningEffect;

    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    [SerializeField] float elapsedTime = 0f;
    [SerializeField] float totalTime = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            totalTime = collision.GetComponent<HandMoving>().waitTime;

            warningEffect.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            elapsedTime = 0f;
            totalTime = 0f;

            warningEffect.SetActive(false);
            collision.GetComponent<HandMoving>().collider.enabled = false;
            collision = null;
        }
    }

    private void Update()
    {
        if (totalTime <= 0) return;

        elapsedTime += Time.deltaTime;
        float t = elapsedTime / totalTime;

        warningEffect.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, t);
    }
}
