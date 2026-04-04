using UnityEngine;

public class QuestConditionContext
{
    public QuestConditionType type;

    // obtain
    public ItemIds itemID;
    public int itemCntToObtain;
    public bool removeItemsOnComplete;

	// kill
	public EnemyIds enemyId;
	public int enemyCntToKill;
}
