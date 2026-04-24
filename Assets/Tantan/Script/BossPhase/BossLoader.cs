using System;
using System.Collections;
using UnityEngine;

public class BossLoader : MonoBehaviour
{
    [SerializeField] Animator transAnimator;
    [SerializeField] float animTime = 1f;

    public IEnumerator LoadBoss(Action a)
    {
        transAnimator.SetTrigger("BossIn");
        yield return new WaitForSecondsRealtime(animTime);
        transAnimator.SetTrigger("BossOut");
        a?.Invoke();
    }

    public void Load(Action action)
    {
        StartCoroutine(LoadBoss(action));
    }

    public void CamShake()
    {
        Camera.main.GetComponent<CameraShake>().Shake(.1f, .5f, CameraShake.ShakeStyle.Vigorous);
    }
}