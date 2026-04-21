using UnityEngine;

public class Totorial_Eat : Minigame
{
    [Header("Minigame")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Transform hand;
    [SerializeField] Transform handGuide;
    [SerializeField] SpriteRenderer kaow;
    [SerializeField] SpriteRenderer food;
    [SerializeField] int halfCount = 2;

    [Header("Sprite")]
    [SerializeField] Sprite[] foodSprite;
    [SerializeField] Sprite[] kaowNaiJaanSprite;

    bool isTukked = false;
    int count = 0;

    private void Start()
    {
        NormalPhaseManager.Instance.OnChangeNote += OnChangedNote;
    }

    public override void Bad()
    {
        kaow.gameObject.SetActive(isTukked ? false : true);
        kaow.sprite = foodSprite[2];
    }

    public override void Good()
    {
        kaow.gameObject.SetActive(isTukked ? false : true);
        kaow.sprite = foodSprite[1];
    }

    public override void Interval()
    {
        float distance = 0;

        if (NormalPhaseManager.Instance.currentBrick)
            distance = Vector2.Distance(NormalPhaseManager.Instance.currentBrick.transform.position, perfectPoint.position);

        float macDistance = Vector2.Distance(NormalPhaseManager.Instance.brickStartPoint, perfectPoint.position);

        if (!isTukked)
            hand.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -39, distance / macDistance));
        else
            hand.eulerAngles = new Vector3(0, 0, Mathf.Lerp(-39, 0, distance / macDistance));
    }

    public override void Miss()
    {
        kaow.gameObject.SetActive(false);
    }

    public override void Perfect()
    {
        kaow.gameObject.SetActive(isTukked ? false : true);
        kaow.sprite = foodSprite[0];
    }

    public override void Reset()
    {

    }

    public override void Tick()
    {
        Interval();
        GuideVisual();
        FoodVisual();
    }
    void GuideVisual()
    {
        handGuide.transform.eulerAngles = new Vector3(0,0,isTukked ? -34.25f : -4.5f);
    }

    void FoodVisual()
    {
        food.sprite = kaowNaiJaanSprite[(count >= halfCount) ? 1 : 0];
    }

    void OnChangedNote()
    {
        isTukked = !isTukked;

        if (isTukked) count++;
    }
}
