using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class QuestConditionProgress
{
	public QuestConditionType type;
	public bool isComplete;

	public string targetName;
	public string UIText
	{
		get
		{
			string s = "";
			switch (type)
			{
				case QuestConditionType.Obtain: s = " 얻기"; break;
                case QuestConditionType.Kill: s = " 처지하기"; break;
            }
			s = String.Concat(targetName, s, $"({curAmount} / {goalAmount})");
			return s;
		}
	}
	public int goalAmount;
	public int curAmount;

	// obtain
	public ItemIds itemID;

	// kill
	public EnemyIds enemyId;

	public static QuestConditionProgress GetObtainConditionProgress(ItemIds itemId, int goalAmount)
	{
		QuestConditionProgress progress = new QuestConditionProgress();
		progress.type = QuestConditionType.Obtain;
		progress.itemID = itemId;
        progress.goalAmount = goalAmount;
        progress.targetName = Enum.GetName(typeof(ItemIds), itemId);

		return progress;
	}
	public static QuestConditionProgress GetKillConditionProgress(EnemyIds enemyId, int goalAmount)
	{
		QuestConditionProgress progress = new QuestConditionProgress();
		progress.type = QuestConditionType.Kill;
		progress.enemyId = enemyId;
		progress.goalAmount = goalAmount;
        progress.targetName = Enum.GetName(typeof(EnemyIds), enemyId);

        return progress;
	}
}
