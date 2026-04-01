using System.ComponentModel;
using UnityEngine;

public class QuestItemReward : QuestReward
{
	QuestItemRewardSO itemRewardSO;

	public QuestItemReward(QuestItemRewardSO itemRewardSO)
		: base(QuestRewardType.Item)
	{
		this.itemRewardSO = itemRewardSO;
	}

	public override void GiveRewardToRunner(IQuestRunner runner)
	{
		runner.AddItem(itemRewardSO.itemData.id, itemRewardSO.itemData, itemRewardSO.itemCount);
	}
}
