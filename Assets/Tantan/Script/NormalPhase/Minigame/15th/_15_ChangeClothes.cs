using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class _15_ChangeClothes : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform curtain;
    [SerializeField] GameObject curtainGuide;
    [SerializeField] GameObject openCurtain;
    [SerializeField] SpriteRenderer girl;
    [SerializeField] SpriteRenderer aura;
    [SerializeField] SpriteRenderer charResult;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] girlSprite;
    [SerializeField] Sprite[] auraSprite;
    [SerializeField] Sprite[] charSprite;

    public override void Bad()
    {
        StartCoroutine(Result(2, resultTime));
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

        Vector3 startScale = new Vector3(.2f, 1, 1); // adjust
        Vector3 endScale = Vector3.one; // final scale

        curtain.localScale = Vector3.Lerp(startScale, endScale, t);
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
        int ranVal = Random.Range(0, girlSprite.Length - 1);

        curtain.gameObject.SetActive(false);
        curtainGuide.SetActive(false);
        openCurtain.SetActive(true);

        switch (type)
        {
            case 0: //P
                girl.sprite = girlSprite[ranVal];
                aura.sprite = auraSprite[0];
                aura.gameObject.SetActive(true);
                charResult.sprite = charSprite[1];
                break;
            case 1: //G
                girl.sprite = girlSprite[ranVal];
                aura.gameObject.SetActive(false);
                charResult.sprite = charSprite[0];
                break;
            case 2: //B
                girl.sprite = girlSprite[ranVal];
                aura.gameObject.SetActive(false);
                charResult.sprite = charSprite[0];
                break;
            case 3: //M
                girl.sprite = girlSprite[3];
                aura.sprite = auraSprite[1];
                aura.gameObject.SetActive(true);
                charResult.sprite = charSprite[2];
                break;
        }
        yield return new WaitForSeconds(duration);
        curtain.gameObject.SetActive(true);
        curtainGuide.SetActive(true);
        openCurtain.SetActive(false);
        aura.gameObject.SetActive(false);
        charResult.sprite = charSprite[0];
    }
}
