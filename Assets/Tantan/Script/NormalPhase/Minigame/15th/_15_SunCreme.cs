using System;
using System.Collections;
using System.Net;
using UnityEngine;

public class _15_SunCreme : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform hand;
    [SerializeField] GameObject handGuide;
    [SerializeField] SpriteRenderer effect;
    [SerializeField] SpriteRenderer expression;
    [SerializeField] Transform[] creamPoints;
    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 endPoint;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] expressionSprite;
    int count = 0;
    int Count 
    {
        get { return count % creamPoints.Length; }
        set
        {
            count = value;
        }
    }

    private void OnEnable()
    {
        NormalPhaseManager.Instance.OnChangeNote += OnChangedNote;

        startPoint = new Vector2(6, -3);
        endPoint = creamPoints[1].position;
    }

    private void OnChangedNote()
    {
        Count++;
        startPoint = creamPoints[Count].position;
        endPoint = creamPoints[(Count + 1) % creamPoints.Length].position;
        creamPoints[Count].GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void Bad()
    {

    }

    public override void Good()
    {

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

        hand.position = Vector3.Lerp(startPoint, endPoint, t);
        handGuide.transform.position = endPoint;

        if (Count == 0)
        {
            foreach (Transform x in creamPoints)
            {
                x.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public override void Miss()
    {
        StartCoroutine(Result(3, resultTime));
    }

    public override void Perfect()
    {
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
        switch (type)
        {
            case 0: //P
                effect.gameObject.SetActive(true);
                expression.sprite = expressionSprite[1];
                break;
            case 1: //G
                effect.gameObject.SetActive(false);
                expression.sprite = expressionSprite[2];
                break;
            case 2: //B
                effect.gameObject.SetActive(false);
                expression.sprite = expressionSprite[3];
                break;
            case 3: //M
                effect.gameObject.SetActive(false);
                expression.sprite = expressionSprite[4];
                break;
        }
        yield return new WaitForSeconds(duration);
        effect.gameObject.SetActive(false);
        expression.sprite = expressionSprite[0];
    }
}