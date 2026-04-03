using UnityEngine;

public class EnemyKilledEvent
{
    public EnemyIds enemyId;
    public int enemyKilledCnt;

    public EnemyKilledEvent(
        EnemyIds enemyId, 
        int enemyKilledCnt)
    {
        this.enemyId = enemyId;
        this.enemyKilledCnt = enemyKilledCnt;
    }
}
