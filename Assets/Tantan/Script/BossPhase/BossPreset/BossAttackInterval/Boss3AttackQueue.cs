public class Boss3AttackQueue : AttackQueue
{
    void OnEnable()
    {
        //attackQueue.Enqueue(new PewPewDiagon(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));
        //attackQueue.Enqueue(new PewPewScatter(bb.projectilePrefab[0], bb.innerSpawnPoints[0], bb));
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[2], bb.innerSpawnPoints, bb, 5));
    }
}