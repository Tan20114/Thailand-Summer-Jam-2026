using System;
using System.Collections;
using UnityEngine;

public class BossLoader : MonoBehaviour
{
    [SerializeField] Animator transAnimator;
    [SerializeField] float animTime = 1f;

    public IEnumerator LoadBoss()
    {
        transAnimator.SetTrigger("BossIn");
        yield return new WaitForSecondsRealtime(animTime);
        transAnimator.SetTrigger("BossOut");
    }

    public void Load()
    {
        StartCoroutine(LoadBoss());
    }

    public void CamShake()
    {
        Camera.main.GetComponent<CameraShake>().Shake(.1f, .5f, CameraShake.ShakeStyle.Vigorous);
    }
}