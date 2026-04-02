using Unity.VisualScripting;
using UnityEngine;

public class QuestConditionProgress
{
	public QuestConditionType type;
	public bool isComplete;

	// obtain
	public ItemIds itemID;
	public int itemCount;

	// kill
	public EnemyIds enemyId;
	public int enemyCnt;

	public static QuestConditionProgress GetObtainConditionProgress(ItemIds itemId)
	{
		QuestConditionProgress progress = new QuestConditionProgress();
		progress.type = QuestConditionType.Obtain;
		progress.itemID = itemId;
		return progress;
	}
	public static QuestConditionProgress GetKillConditionProgress(EnemyIds enemyId)
	{
		QuestConditionProgress progress = new QuestConditionProgress();
		progress.type = QuestConditionType.Kill;
		progress.enemyId = enemyId;
		return progress;
	}
}
