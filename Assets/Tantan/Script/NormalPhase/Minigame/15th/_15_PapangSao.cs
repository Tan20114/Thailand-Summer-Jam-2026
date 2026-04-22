using System.Collections;
using UnityEngine;

public class _15_PapangSao : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform hand;
    [SerializeField] GameObject handGuide;
    [SerializeField] SpriteRenderer character;
    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 endPoint;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] characterSprite;

    bool isGirl = false;

    private void Start()
    {
        int ranVal = Random.Range(0, 2);
        isGirl =  ranVal > 0 ? true : false;

        startPoint = hand.transform.position;
        endPoint = handGuide.transform.position;

        character.sprite = characterSprite[(ranVal * 4) + 1];
    }

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

        hand.position = Vector3.Lerp(startPoint, endPoint, t);
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
        GetComponent<Animator>().SetTrigger("hit");

        hand.gameObject.SetActive(false);
        handGuide.SetActive(false);

        switch (type)
        {
            case 0: // P
                character.sprite = characterSprite[0 + (isGirl ? 4 : 0)];
                break;
            case 1: //G
                character.sprite = characterSprite[1 + (isGirl ? 4 : 0)];
                break;
            case 2: //B
                character.sprite = characterSprite[2 + (isGirl ? 4 : 0)];
                break;
            case 3: //M
                character.sprite = characterSprite[3 + (isGirl ? 4 : 0)];
                break;
        }
        yield return new WaitForSeconds(duration);
        hand.gameObject.SetActive(true);
        handGuide.SetActive(true);
        isGirl = !isGirl;
        character.sprite = characterSprite[1 + (isGirl ? 4 : 0)];
    }
}
