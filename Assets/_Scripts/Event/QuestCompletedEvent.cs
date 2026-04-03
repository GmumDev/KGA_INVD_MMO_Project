using UnityEngine;

public class QuestCompletedEvent
{
	public string title;
	public QuestRewardContext[] rewardContexts;

	public QuestCompletedEvent(
		string title,
		QuestRewardContext[] rewardContexts)
	{
		this.title = title;
		this.rewardContexts = rewardContexts;
	}
}
