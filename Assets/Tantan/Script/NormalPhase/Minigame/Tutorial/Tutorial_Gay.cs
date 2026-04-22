using System.Collections;
using System.Net;
using UnityEngine;

public class Tutorial_Gay : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform gay;
    [SerializeField] Transform gayGuide;
    [SerializeField] SpriteRenderer character;
    [SerializeField] GameObject result;
    [SerializeField] float resultTime = 1f;

    Vector2 gayStartPos;
    Vector2 gayEndPos;

    private void Start()
    {
        gayStartPos = gay.position;
        gayEndPos = gayGuide.position;
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

        float currentX = NormalPhaseManager.Instance.currentBrick.transform.position.x;
        float startX = NormalPhaseManager.Instance.brickStartPoint.x;
        float endX = -0.25f;

        float t = Mathf.InverseLerp(startX, endX, currentX);
        t = Mathf.Clamp01(t);

        Vector3 basePos = Vector3.Lerp(gayStartPos, gayEndPos, t);

        Vector3 dir = (gayEndPos - gayStartPos).normalized;

        Vector3 perp = new Vector3(-dir.y, dir.x, 0f);

        float wobbleSpeed = 6f;
        float wobbleAmount = .25f;

        float wobble =
            Mathf.Sin(Time.time * wobbleSpeed) +
            (Mathf.PerlinNoise(Time.time, 0f) - 0.5f);

        wobble *= wobbleAmount;

        gay.position = basePos + perp * wobble;
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
        gayGuide.gameObject.SetActive(false);

        switch (type)
        {
            case 0:
                gay.gameObject.SetActive(true);
                character.transform.position += new Vector3(1, 0, 0) * 2;
                result.SetActive(false);
                break;
            case 1:
                gay.gameObject.SetActive(true);
                character.transform.position += new Vector3(1, 0, 0) * 2;
                result.SetActive(false);
                break;
            case 2:
                gay.gameObject.SetActive(false);
                result.SetActive(true);
                character.enabled = false;
                break;
            case 3:
                gay.gameObject.SetActive(false);
                result.SetActive(true);
                character.enabled = false;
                break;
        }

        yield return new WaitForSeconds(duration);
        gay.gameObject.SetActive(true);
        gayGuide.gameObject.SetActive(true);
        character.enabled = true;
        character.transform.position = Vector3.zero;
        result.SetActive(false);
    }
}