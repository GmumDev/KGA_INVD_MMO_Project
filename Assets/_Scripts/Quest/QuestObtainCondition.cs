using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestObtainCondition: QuestCondition
{
	QuestObtainConditionSO obtainSO;
	
	public QuestObtainCondition(QuestObtainConditionSO obtainSO)
		:base(QuestConditionType.Obtain)
	{
		this.obtainSO = obtainSO;
	}

	public override bool IsMet(IQuestRunner runner)
	{
		return runner.GetItemCount(obtainSO.itemID) >= obtainSO.itemCount;
	}
}
