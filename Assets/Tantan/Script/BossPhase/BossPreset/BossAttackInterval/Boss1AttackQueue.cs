public class Boss1AttackQueue : AttackQueue
{
    void OnEnable()
    {
        attackQueue.Enqueue(new EyeShoot(bb.projectilePrefab[0], bb.innerSpawnPoints[0], bb));
        attackQueue.Enqueue(new PewPew1(bb.projectilePrefab[1], bb.innerSpawnPoints[0], bb));
        attackQueue.Enqueue(new PewPew2(bb.projectilePrefab[1], bb.innerSpawnPoints[0],bb));
        attackQueue.Enqueue(new HandSwipe(bb.projectilePrefab[2], bb.outerSpawnPoints, bb));
        attackQueue.Enqueue(new HandStompAttack(bb.projectilePrefab[3], bb.outerSpawnPoints, bb));
    }
}
