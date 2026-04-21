using UnityEngine;
using System.Collections;

public class _15_ChangeClothes : Minigame
{
    public override void Bad()
    {

    }

    public override void Good()
    {

    }

    public override void Interval()
    {

    }

    public override void Miss()
    {

    }

    public override void Perfect()
    {

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
        yield return new WaitForSeconds(duration);
    }
}
