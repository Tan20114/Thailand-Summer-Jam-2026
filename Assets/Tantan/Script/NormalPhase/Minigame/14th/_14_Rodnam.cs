using UnityEngine;
using System.Collections;

public class _14_Rodnam : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform Kan;
    [SerializeField] GameObject kanGuide;
    [SerializeField] SpriteRenderer charSR;
    [SerializeField] SpriteRenderer bubble;
    [SerializeField] GameObject[] puddle;
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] charSprite;
    [SerializeField] Sprite[] bubbleSprite;

    public override void Bad()
    {
        StartCoroutine(Result(2,resultTime));
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

        Vector3 EndPosition = new Vector3(0 , 1.5f);

        Kan.position = Vector3.Lerp(new Vector2(0, -3.5f), EndPosition, t);
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
        Kan.GetComponent<SpriteRenderer>().enabled = false;
        kanGuide.SetActive(false);
        switch (type)
        {
            case 0: //P
                charSR.sprite = charSprite[1];
                bubble.gameObject.SetActive(true);
                bubble.sprite = bubbleSprite[0];
                puddle[0].SetActive(true);
                puddle[1].SetActive(false);
                break;
            case 1: //G
                charSR.sprite = charSprite[0];
                bubble.gameObject.SetActive(false);
                puddle[0].SetActive(true);
                puddle[1].SetActive(false);
                break;
            case 2: //B
                charSR.sprite = charSprite[0];
                bubble.gameObject.SetActive(false);
                puddle[0].SetActive(false);
                puddle[1].SetActive(true);
                break;
            case 3: //M
                charSR.sprite = charSprite[2];
                bubble.gameObject.SetActive(true);
                bubble.sprite = bubbleSprite[1];
                puddle[0].SetActive(false);
                puddle[1].SetActive(true);
                break;
        }
        yield return new WaitForSeconds(duration);
        Kan.GetComponent<SpriteRenderer>().enabled = true;
        kanGuide.SetActive(true);
        charSR.sprite = charSprite[0];
        bubble.gameObject.SetActive(false);
        foreach (GameObject g in puddle)
            g.SetActive(false);
    }

}
