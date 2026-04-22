using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.XR;

public class _13_Sandal : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform hand;
    [SerializeField] GameObject handGuide;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] SpriteRenderer leg;
    [SerializeField] SpriteRenderer effect;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] legSprite;
    [SerializeField] Sprite[] effectSprite;

    Vector2 startPoint;
    Vector2 endPoint;

    private void OnEnable()
    {
        startPoint = hand.position;
        endPoint = handGuide.transform.position;
    }

    public override void Bad()
    {
        StartCoroutine(Result(2, resultTime));
    }

    public override void Good()
    {
        StartCoroutine(Result(1,resultTime));
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
    }

    public override void Miss()
    {
        StartCoroutine(Result(3,resultTime));
    }

    public override void Perfect()
    {
        StartCoroutine(Result(0,resultTime));
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
        hand.gameObject.SetActive(false);
        handGuide.SetActive(false);

        switch(type)
        {
            case 0:
                charSR.sprite = charSprite[1];
                leg.sprite = legSprite[1];
                effect.sprite = effectSprite[0];
                effect.gameObject.SetActive(true);
                break;
            case 1:
                charSR.sprite = charSprite[1];
                leg.sprite = legSprite[1];
                effect.gameObject.SetActive(false);
                break;
            case 2:
                charSR.sprite = charSprite[2];
                leg.sprite = legSprite[2];
                effect.gameObject.SetActive(false);
                break;
            case 3:
                charSR.sprite = charSprite[2];
                leg.sprite = legSprite[2];
                effect.sprite = effectSprite[1];
                effect.gameObject.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(duration);
        hand.gameObject.SetActive(true);
        handGuide.SetActive(true);
        charSR.sprite = charSprite[0];
        leg.sprite = legSprite[0];
        effect.gameObject.SetActive(false);
    }
}
