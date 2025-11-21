public interface IPlantRangedAttacks{
    void RangedAttack();

    short numProjectiles{get;}
    float spreadAngle{get;}
    float projectileSpeed{get;}
    int pierceCount{get;}
    bool ignoreObstacles{get;}
    bool zombieInLane{get;}
}