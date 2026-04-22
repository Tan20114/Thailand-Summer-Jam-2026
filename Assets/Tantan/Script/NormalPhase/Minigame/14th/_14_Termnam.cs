using UnityEngine;
using System.Collections;

public class _14_Termnam : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform wongDeang;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] SpriteRenderer result;
    [SerializeField] SpriteRenderer result2;
    [SerializeField] SpriteRenderer result3;
    [SerializeField] float resultTime = 1;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] resultSprite;
    [SerializeField] Sprite[] result2Sprite;
    [SerializeField] Sprite[] result3Sprite;

    public override void Bad()
    {
        StartCoroutine(Result(3, resultTime));
    }

    public override void Good()
    {
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

        Vector3 startScale = Vector3.one * 2;
        Vector3 endScale = Vector3.one;

        Vector3 EndPosition = result3.transform.position;

        wongDeang.localScale = Vector3.Lerp(startScale, endScale, t);
        wongDeang.position = Vector3.Lerp(new Vector2(-11,3), new Vector2(1.8f,4.45f), t);
    }

    public override void Miss()
    {
        StartCoroutine(Result(4, resultTime));
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
            case 0: // P
                result.sprite = resultSprite[1];
                result2.gameObject.SetActive(true);
                result2.sprite = result2Sprite[0];
                result3.sprite = result3Sprite[1];
                charSR.sprite = charSprite[0];
                charSR.gameObject.SetActive(true);
                break;
            case 1: // G
                result.sprite = resultSprite[1];
                result2.gameObject.SetActive(true);
                result2.sprite = result2Sprite[1];
                result3.sprite = result3Sprite[1];
                break;
            case 3: // B
                result.sprite = resultSprite[0];
                result2.gameObject.SetActive(true);
                result2.sprite = result2Sprite[2];
                result3.sprite = result3Sprite[1];
                break;
            case 4: // M
                result.sprite = resultSprite[0];
                result2.gameObject.SetActive(false);
                result3.sprite = result3Sprite[1];
                charSR.sprite = charSprite[1];
                charSR.gameObject.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(duration);
        charSR.gameObject.SetActive(false);
        result.sprite = resultSprite[0];
        result2.gameObject.SetActive(false);
        result3.sprite = result3Sprite[0];
    }
}
