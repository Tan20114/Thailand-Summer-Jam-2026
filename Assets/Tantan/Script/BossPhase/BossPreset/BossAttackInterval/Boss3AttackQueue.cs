public class Boss3AttackQueue : AttackQueue
{
    void OnEnable()
    {
        // Phase 1
        attackQueue.Enqueue(new PewPewDiagon(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));

        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));

        attackQueue.Enqueue(new PewPewScatter(bb.projectilePrefab[0], bb.innerSpawnPoints[0], bb));//78
        attackQueue.Enqueue(new PewPewDiagon(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));
        attackQueue.Enqueue(new PewPewScatter(bb.projectilePrefab[0], bb.innerSpawnPoints[0], bb));
        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));//110
        

        // Phase 2 (122)
        
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[2], bb.innerSpawnPoints, bb, 2));
        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[2], bb.innerSpawnPoints, bb, 3));
        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[2], bb.innerSpawnPoints, bb, 4));
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[2], bb.innerSpawnPoints, bb, 1));

    }
}