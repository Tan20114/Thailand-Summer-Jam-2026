using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform perfectPoint;
    public Vector2 comboPos;

    public abstract void Perfect();
    public abstract void Good();
    public abstract void Bad();
    public abstract void Miss();
    public abstract void Tick();
    public abstract void Interval();
    public abstract void Reset();
}
