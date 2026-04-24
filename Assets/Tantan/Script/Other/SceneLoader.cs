using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transAnimator;
    [SerializeField] float animTime = 1f;

    public IEnumerator LoadScene(int index)
    {
        transAnimator.SetTrigger("SceneOut");
        yield return new WaitForSecondsRealtime(animTime);
        SceneManager.LoadScene(index);
    }

    public void Load(int index)
    {
        StartCoroutine(LoadScene(index));
    }
}