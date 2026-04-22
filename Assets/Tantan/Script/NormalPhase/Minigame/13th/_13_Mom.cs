using System.Collections;
using UnityEngine;

public class _13_Mom : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform wongDeang;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] SpriteRenderer result;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] resultSprite;

    public override void Bad()
    {
        StartCoroutine(Result(1, resultTime));
    }

    public override void Good()
    {
        StartCoroutine(Result(0,resultTime));
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

        if (t < 0.33f)
        {
            charSR.sprite = charSprite[0];
        }
        else if (t < 0.67f)
        {
            charSR.sprite = charSprite[1];
        }
        else
        {
            charSR.sprite = charSprite[2];
        }

        Vector3 startScale = Vector3.one * 2;
        Vector3 endScale = new Vector3(0.7f, 0.7f, 0.7f);

        wongDeang.localScale = Vector3.Lerp(startScale, endScale, t);
    }

    public override void Miss()
    {
        StartCoroutine(Result(1,resultTime));
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

    IEnumerator Result(int index, float duration)
    {
        charSR.sprite = charSprite[index];
        result.sprite = resultSprite[index];
        charSR.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
        if (index == 1)
        {
            GetComponent<Animator>().SetTrigger("Miss");
        }
        yield return new WaitForSeconds(duration);
        charSR.gameObject.SetActive(true);
        result.gameObject.SetActive(false);
        result.transform.position = Vector2.zero;
        charSR.sprite = charSprite[0];
        result.sprite = resultSprite[0];
    }
}
