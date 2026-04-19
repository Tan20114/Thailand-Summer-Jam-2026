public class Boss2AttackQueue : AttackQueue
{
    void OnEnable()
    {
        attackQueue.Enqueue(new WaterPeedPeed(bb.projectilePrefab[0], bb.outerSpawnPoints, bb));
    }
}
