using System.Collections;
using UnityEngine;

public class _13_Dood : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform waterSuck;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] SpriteRenderer charBG;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] charBg;

    public override void Bad()
    {
        StartCoroutine(Result(1, resultTime));
    }

    public override void Good()
    {
        StartCoroutine(Result(2, resultTime));
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

        waterSuck.position = Vector3.Lerp(
            startPoint.position,
            endPoint.position,
            t
        );
    }

    public override void Miss()
    {
        StartCoroutine(Result(1,resultTime));
    }

    public override void Perfect()
    {
        StartCoroutine(Result(3,resultTime));
    }

    public override void Reset()
    {
        
    }

    public override void Tick()
    {
        Interval();
    }

    IEnumerator Result(int index, float duration)
    {
        charSR.sprite = charSprite[index];
        charBG.sprite = charBg[index];
        yield return new WaitForSeconds(duration);
        charSR.sprite = charSprite[0];
        charBG.sprite = charBg[0];
    }
}
