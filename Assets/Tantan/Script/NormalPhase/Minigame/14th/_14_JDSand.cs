using System;
using System.Collections;
using UnityEngine;

public class _14_JDSand : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform sand;
    [SerializeField] SpriteRenderer sandTier;
    [SerializeField] GameObject sandGuide;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] GameObject sandEffect;
    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 endPoint;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] sandSprite;
    [SerializeField] Sprite[] characterSprite;

    int tier = 0;
    int Tier
    {
        get { return tier; }
        set
        {
            tier = Mathf.Clamp(value, 0, sandSprite.Length - 1);
        }
    }

    public override void Bad()
    {
        StartCoroutine(Result(2, resultTime));
    }

    public override void Good()
    {
        Tier++;
        StartCoroutine(Result(1, resultTime));
    }

    public override void Interval()
    {
        if (!NormalPhaseManager.Instance.currentBrick)
            return;

        float distance = Vector2.Distance(
            NormalPhaseManager.Instance.currentBrick.transform.position,
            perfectPoint.position
        );

        float maxDistance = Vector2.Distance(
            NormalPhaseManager.Instance.brickStartPoint,
            perfectPoint.position
        );

        float currentX = NormalPhaseManager.Instance.currentBrick.transform.position.x;
        float startX = NormalPhaseManager.Instance.brickStartPoint.x;
        float endX = -0.25f;

        float t = Mathf.InverseLerp(startX, endX, currentX);
        t = Mathf.Clamp01(t);

        sand.position = Vector3.Lerp(startPoint, endPoint, t);
        sand.GetComponent<SpriteRenderer>().sprite = sandSprite[Tier];
        sandGuide.GetComponent<SpriteRenderer>().sprite = sandSprite[Tier];
        if (Tier >= sandSprite.Length - 1)
            sandTier.sprite = sandSprite[sandSprite.Length - 1];
        else if (Tier > 0)
            sandTier.sprite = sandSprite[Tier - 1];
        else
            sandTier.sprite = null;
    }

    public override void Miss()
    {
        StartCoroutine(Result(3, resultTime));
    }

    public override void Perfect()
    {
        Tier++;
        StartCoroutine(Result(0, resultTime));
    }

    public override void Reset()
    {

    }

    public override void Tick()
    {
        Interval();
    }
    IEnumerator Result(int type, float duration)
    {
        sand.gameObject.SetActive(false);
        sandGuide.SetActive(false);

        switch (type)
        {
            case 0:
                charSR.sprite = characterSprite[1];
                sandEffect.SetActive(true);
                break;
            case 1:
                charSR.sprite = characterSprite[0];
                sandEffect.SetActive(false);
                break;
            case 2:
                charSR.sprite = characterSprite[0];
                sandEffect.SetActive(false);
                break;
            case 3:
                charSR.sprite = characterSprite[2];
                sandEffect.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(duration);
        sand.gameObject.SetActive(true);
        sandGuide.SetActive(true);
        charSR.sprite = characterSprite[0];
        sandEffect.SetActive(false);
    }
}