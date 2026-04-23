using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class _14_TermPeun : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform wong;
    [SerializeField] GameObject wongGuide;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] SpriteRenderer waterFill;
    [SerializeField] SpriteRenderer waterFlow;
    [SerializeField] float resultDuration = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] waterFillSprite;
    [SerializeField] Sprite[] waterFlowSprite;

    int count = 0;
    int Count
    {
        get => count % waterFillSprite.Length;
        set => count = value;
    }

    public override void Bad()
    {
        StartCoroutine(Result(2, resultDuration));
    }

    public override void Good()
    {
        Count++;
        StartCoroutine(Result(1, resultDuration));
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

        Vector3 startScale = Vector3.one * 7;
        Vector3 endScale = new Vector3(1.95f, 1.95f, 1.95f);

        wong.localScale = Vector3.Lerp(startScale, endScale, t);
        waterFill.sprite = waterFillSprite[Count];
    }

    public override void Miss()
    {
        StartCoroutine(Result(3, resultDuration));
    }

    public override void Perfect()
    {
        Count++;
        StartCoroutine(Result(0, resultDuration));
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
            case 0:
                charSR.sprite = charSprite[0];
                waterFlow.sprite = waterFlowSprite[0];
                break;
            case 1:
                charSR.sprite = charSprite[1];
                waterFlow.sprite = waterFlowSprite[1];
                break;
            case 2:
                charSR.sprite = charSprite[2];
                waterFlow.sprite = waterFlowSprite[2];
                break;
            case 3:
                charSR.sprite = charSprite[2];
                waterFlow.sprite = null;
                break;
        }
        yield return new WaitForSeconds(duration);
        charSR.sprite = charSprite[1];
        waterFlow.sprite = null;
    }
}
