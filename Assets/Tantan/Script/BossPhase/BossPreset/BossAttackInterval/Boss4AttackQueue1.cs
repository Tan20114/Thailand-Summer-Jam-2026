public class Boss4AttackQueue : AttackQueue
{
    void OnEnable()
    {
        // Phase 1
        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));//กระสุนรัวๆ
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[1], bb.innerSpawnPoints, bb, 1));//Laserกั้นสี่มุม
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[0], bb.innerSpawnPoints, bb, 1));//Laserแปลกๆหมุนๆเท่ดี
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[0], bb.innerSpawnPoints, bb, 4));//Laserดาบหมุนๆ
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[0], bb.innerSpawnPoints, bb, 1));//Laserหมุนสี่มุม
        attackQueue.Enqueue(new PewPewDiagon(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));//diagon
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[0], bb.innerSpawnPoints, bb, 1));//Laserหมุนสี่มุม
        attackQueue.Enqueue(new PewPewAround(bb.projectilePrefab[0], bb.innerSpawnPoints, bb));//กระสุนรัวๆ

        // Phase 2 (177)
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[0], bb.innerSpawnPoints, bb, 4));//Laserดาบหมุนๆ
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[1], bb.innerSpawnPoints, bb, 5));//Laserหมุนสี่มุม
        attackQueue.Enqueue(new SpinningWater(bb.projectilePrefab[0], bb.innerSpawnPoints, bb, 1));//Laserหมุนสี่มุม

    }
}