using UnityEngine;

public class QuestConditionContext
{
    public QuestConditionIds id;
    public QuestConditionType type;

    // obtain
    public ItemIds itemID;
    public int itemCount;
    public bool removeItemsOnComplete;

	// kill
	public EnemyIds enemyId;
	public int enemyCntToKill;
}
