public class Boss2AttackQueue : AttackQueue
{
    void OnEnable()
    {
        //Phase 1
        attackQueue.Enqueue(new WaterPeedPeed(bb.projectilePrefab[0], bb.outerSpawnPoints, bb));
        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));

        //Phase 2
        attackQueue.Enqueue(new BigWaterPeedPeed(bb.projectilePrefab[1], bb.outerSpawnPoints, bb));
        attackQueue.Enqueue(new MovingPeedPeed(bb.projectilePrefab[2], bb.outerSpawnPoints, bb));
        attackQueue.Enqueue(new ShockwaveAttack(bb.projectilePrefab[3], bb.innerSpawnPoints, bb));
        attackQueue.Enqueue(new MovingPeedPeed(bb.projectilePrefab[2], bb.outerSpawnPoints, bb));
        attackQueue.Enqueue(new BigWaterPeedPeed(bb.projectilePrefab[1], bb.outerSpawnPoints, bb));
        attackQueue.Enqueue(new ShockwaveAttack(bb.projectilePrefab[3], bb.innerSpawnPoints, bb));

    }
}