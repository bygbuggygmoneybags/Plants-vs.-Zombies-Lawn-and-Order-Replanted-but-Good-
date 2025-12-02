public interface IPlantRangedAttacks
{
    void RangedAttack(int numProjectiles, float spreadAngle, float speed, int piercingNum, bool ignoreObstacles, bool zombieInLane);

    short NumProjectiles{ get; set; }
    float SpreadAngle{ get; set; }
    float ProjectileSpeed{ get; set; }
    int PierceCount{ get; set; }
    bool IgnoreObstacles{ get; set; }
    bool ZombieInLane{ get; set; }
}