using System.Collections;
using UnityEngine;

public class _13_Cold : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform water;
    [SerializeField] SpriteRenderer charSr;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;

    private void Start()
    {
        NormalPhaseManager.Instance.OnChangeNote += OnChangedNote;
    }

    public override void Bad()
    {
        StartCoroutine(Result(false, 1f));
    }

    public override void Good()
    {
        StartCoroutine(Result(true, 1f));
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

        Vector3 startScale = new Vector3(2.5f, 2.5f, 2.5f); // adjust
        Vector3 endScale = Vector3.one; // final scale

        water.localScale = Vector3.Lerp(startScale, endScale, t);

        float startZ = -11f; // adjust
        float endZ = 0f;    // target depth

        Vector3 pos = water.localPosition;
        pos.z = Mathf.Lerp(startZ, endZ, t);
        water.localPosition = pos;
    }

    public override void Miss()
    {
        StartCoroutine(Result(false, 1f));
    }

    public override void Perfect()
    {
        StartCoroutine(Result(true, 1f));
    }

    public override void Reset()
    {

    }

    public override void Tick()
    {
        Interval();
    }

    void OnChangedNote()
    {
    }

    IEnumerator Result(bool isSuccess, float duration)
    {
        charSr.sprite = charSprite[isSuccess ? 0 : 1];
        yield return new WaitForSeconds(duration);
        charSr.sprite = charSprite[1];
    }
}
