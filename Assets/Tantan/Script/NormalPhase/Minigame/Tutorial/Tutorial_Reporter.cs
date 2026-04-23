using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Tutorial_Reporter : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] SpriteRenderer waterGun;
    [SerializeField] Transform[] aimSight;
    [SerializeField] GameObject[] idleObj;
    [SerializeField] GameObject resultObj;
    [SerializeField] SpriteRenderer resultRenderer;
    List<Vector2> position = new List<Vector2>();
    [SerializeField] float resultTime = 1f;

    [Header("Sprite")]
    [SerializeField] Sprite[] watergunSprite;
    [SerializeField] Sprite[] result;

    bool isFlip = false;

    private void Start()
    {
        foreach (Transform t in aimSight)
            position.Add(t.position);
    }

    public override void Bad()
    {
        StartCoroutine(Result(false, resultTime));
    }

    public override void Good()
    {
        StartCoroutine(Result(true, resultTime));
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

        WaterGunVisulize(distance / maxDistance, 0.4f, -0.6f);

        for (int i = 0; i < aimSight.Length; i++)
        {
            aimSight[i].position = Vector2.Lerp(position[i], Vector2.zero, t);
        }
        resultObj.GetComponent<SpriteRenderer>().flipX = isFlip;
    }

    void WaterGunVisulize(float t, float spawnRatio, float shootPoint)
    {
        if (!NormalPhaseManager.Instance.currentBrick)
        {
            waterGun.gameObject.SetActive(false);
            return;
        }

        if (NormalPhaseManager.Instance.currentBrick.transform.position.x >= shootPoint)
            waterGun.sprite = watergunSprite[1];
        else if (t <= spawnRatio)
        {
            waterGun.gameObject.SetActive(true);
            waterGun.sprite = watergunSprite[0];
        }
        else
            waterGun.gameObject.SetActive(false);
    }

    public override void Miss()
    {
        StartCoroutine(Result(false, resultTime));
    }

    public override void Perfect()
    {
        StartCoroutine(Result(true, resultTime));
    }

    public override void Reset()
    {
    }

    public override void Tick()
    {
        Interval();
    }

    IEnumerator Result(bool isSuccess, float duration)
    {
        foreach (GameObject obj in idleObj)
            obj.SetActive(false);
        isFlip = !isFlip;
        resultRenderer.sprite = result[isSuccess ? 0 : 1];
        resultObj.SetActive(true);
        yield return new WaitForSeconds(duration);
        foreach (GameObject obj in idleObj)
            obj.SetActive(true);
        resultObj.SetActive(false);
    }
}
