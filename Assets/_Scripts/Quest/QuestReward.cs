using UnityEngine;

public abstract class QuestReward
{

	protected QuestRewardType type { get; }
	public QuestReward(QuestRewardType type)
	{
		this.type = type;
	}

	public abstract void GiveRewardToRunner(IQuestRunner runner);
}
