using System.Collections.Generic;
using UnityEngine;

public class AttackQueue : MonoBehaviour
{
    public Queue<AttackTemplate> attackQueue = new Queue<AttackTemplate>();
    protected BossBehavior bb => BossBehavior.Instance;
}
