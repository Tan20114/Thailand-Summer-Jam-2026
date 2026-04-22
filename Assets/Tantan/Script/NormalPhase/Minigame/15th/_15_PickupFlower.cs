using UnityEngine;
using System.Collections;

public class _15_PickupFlower : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform hand;
    [SerializeField] GameObject handGuide;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] GameObject ponytail;
    [SerializeField] SpriteRenderer charResult;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject flower;
    [SerializeField] SpriteRenderer handResult;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] bgSprite;
    [SerializeField] Sprite[] resultSprite;

    public override void Bad()
    {
        StartCoroutine(Result(2, resultTime));
    }

    public override void Good()
    {
        StartCoroutine(Result(1, resultTime));
    }

    public override void Miss()
    {
        StartCoroutine(Result(3, resultTime));
    }

    public override void Perfect()
    {
        StartCoroutine(Result(0, resultTime));
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

        hand.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
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
        switch(type)
        {
            case 0: // P
                background.sprite = bgSprite[1];
                hand.gameObject.SetActive(false);
                handGuide.SetActive(false);
                charSR.gameObject.SetActive(false);
                ponytail.SetActive(false);
                charResult.sprite = bgSprite[0];
                charResult.gameObject.SetActive(true);
                GetComponent<Animator>().SetTrigger("Perfect");
                flower.SetActive(false);
                handResult.sprite = resultSprite[0];
                handResult.gameObject.SetActive(true);
                break;
            case 1: // G
                background.sprite = bgSprite[0];
                hand.gameObject.SetActive(false);
                handGuide.SetActive(false);
                charSR.gameObject.SetActive(true);
                ponytail.SetActive(true);
                charResult.gameObject.SetActive(false);
                flower.SetActive(false);
                handResult.sprite = resultSprite[0];
                handResult.gameObject.SetActive(true);
                break;
            case 2: // B
                background.sprite = bgSprite[0];
                hand.gameObject.SetActive(false);
                handGuide.SetActive(false);
                charSR.gameObject.SetActive(false);
                ponytail.SetActive(false);
                charResult.sprite = bgSprite[1];
                charResult.gameObject.SetActive(true);
                GetComponent<Animator>().SetTrigger("Bad");
                flower.SetActive(true);
                handResult.sprite = resultSprite[1];
                handResult.gameObject.SetActive(true);
                break;
            case 3: // M
                background.sprite = bgSprite[2];
                hand.gameObject.SetActive(false);
                handGuide.SetActive(false);
                charSR.gameObject.SetActive(false);
                ponytail.SetActive(false);
                charResult.sprite = charSprite[2];
                charResult.gameObject.SetActive(true);
                GetComponent<Animator>().SetTrigger("Miss");
                flower.SetActive(true);
                handResult.sprite = resultSprite[1];
                handResult.gameObject.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(duration);
        background.sprite = bgSprite[0];
        hand.gameObject.SetActive(true);
        handGuide.SetActive(true);
        charSR.gameObject.SetActive(true);
        ponytail.SetActive(true);
        charResult.gameObject.SetActive(false);
        flower.SetActive(true);
        handResult.gameObject.SetActive(false);
    }
}
