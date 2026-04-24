using System.Collections;
using UnityEngine;

public class TutorialPage : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] float lifetime = 0;

    private void OnEnable()
    {
        GameManager.Instance.isTutorial = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        GameManager.Instance.isTutorial = false;
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(lifetime);
        gameObject.SetActive(false);
    }
}
